using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide;
using RapidRide.Entities;

namespace RapidRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public WithdrawalController(RapidRideDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Withdrawal>> PostWithdrawal(Withdrawal withdrawal)
        {
            // Validate the amount being withdrawn
            var wallet = await _context.Wallets.FindAsync(withdrawal.WalletId);
            if (wallet == null)
            {
                return BadRequest("wallet not found");
            }
            if (withdrawal.Amount > wallet.Balance)
            {
                return BadRequest("Insufficient balance");
            }

            // Deduct the amount from the wallet's balance
            wallet.Balance -= withdrawal.Amount;

            // Add the withdrawal to the database
            _context.Withdrawals.Add(withdrawal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWithdrawal", new { id = withdrawal.WithdrawalId }, withdrawal);
        }
    }

}
