using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace RapidRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly RapidRideDbContext _context;


        public DepositController(RapidRideDbContext context)
        {
            _context = context;
        }
        [HttpGet("api/deposit/{walletId}")]
        public IActionResult GetDepositsByWalletId(int walletId)
        {
            var deposits = _context.Deposits.Where(d => d.WalletId == walletId).ToList();

            if (deposits == null || deposits.Count == 0)
            {
                return NotFound();
            }

            return Ok(deposits);
        }

        [HttpPost]
        public IActionResult RechargeWallet(string rechargeCardNumber, int userId)
        {
            if (ModelState.IsValid)
            {
                // Verify the recharge card number
                RechargeCard rechargeCard = _context.RechargeCards.FirstOrDefault(rc => rc.Number == rechargeCardNumber && rc.IsActive);

                if (rechargeCard != null)
                {
                    // Get the user wallet (assuming the user's ID is stored in a variable called userId)
                    Wallet userWallet = _context.Wallets.FirstOrDefault(w => w.UserId == userId);

                    // Update the wallet balance
                    userWallet.Balance += float.Parse(rechargeCard.Category);
                    rechargeCard.IsActive = false;
                    rechargeCard.DepositId = userWallet.WalletId;

                    // Create a new deposit record
                    Deposit newDeposit = new Deposit()
                    {
                        Amount = float.Parse(rechargeCard.Category),
                        Date = DateTime.Now,
                        WalletId = userWallet.WalletId
                    };

                    _context.Deposits.Add(newDeposit);
                    _context.SaveChanges();
                    return Ok(new { message = "Success", balance = userWallet.Balance, deposit = newDeposit.Amount });
                    //  return RedirectToAction("Success"); // Redirect to the success page
                }
                else
                {
                    ModelState.AddModelError("RechargeCardNumber", "Invalid recharge card number.");
                }
            }

            return BadRequest(ModelState); // Return validation errors as JSON
        }

    }
}
