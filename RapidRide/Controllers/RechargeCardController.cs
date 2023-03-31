using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;

namespace RapidRide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RechargeCardController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public RechargeCardController(RapidRideDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RechargeCard>>> GetRechargeCards()
        {
            return await _context.RechargeCards.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RechargeCard>> GetRechargeCard(int id)
        {
            var rechargeCard = await _context.RechargeCards.FindAsync(id);

            if (rechargeCard == null)
            {
                return NotFound();
            }

            return rechargeCard;
        }
        /*  public ActionResult<IEnumerable<RechargeCard>> GetCardsForWallet(int walletId)
          {
              var activeCards = _context.RechargeCards.Where(c => c.WalletId == walletId && c.IsActive == true).ToList();
              return activeCards;
          }
          */

        [HttpPost]
        public async Task<ActionResult<RechargeCard>> CreateRechargeCard(RechargeCard rechargeCard)
        {
            _context.RechargeCards.Add(rechargeCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRechargeCard), new { id = rechargeCard.Id }, rechargeCard);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRechargeCard(int id, RechargeCard rechargeCard)
        {
            if (id != rechargeCard.Id)
            {
                return BadRequest();
            }

            _context.Entry(rechargeCard).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRechargeCard(int id)
        {
            var rechargeCard = await _context.RechargeCards.FindAsync(id);

            if (rechargeCard == null)
            {
                return NotFound();
            }

            _context.RechargeCards.Remove(rechargeCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
