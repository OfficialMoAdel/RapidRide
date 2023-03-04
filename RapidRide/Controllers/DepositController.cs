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

        [HttpPost]
        public async Task<IActionResult> MakeDeposit(Deposit deposit)
        {
            var wallet = await _context.Wallets.FindAsync(deposit.WalletId);

            if (wallet == null)
            {
                return NotFound();
            }

            wallet.Balance += deposit.Amount;
            _context.Deposits.Add(deposit);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
