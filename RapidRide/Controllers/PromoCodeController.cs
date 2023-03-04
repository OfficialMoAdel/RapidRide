using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace RapidRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public PromoCodeController(RapidRideDbContext context)
        {
            _context = context;
        }

        // GET: api/PromoCode
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PromoCode>>> GetPromoCodes()
        {
            return await _context.PromoCodes.ToListAsync();
        }

        // GET: api/PromoCode/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PromoCode>> GetPromoCode(int id)
        {
            var promoCode = await _context.PromoCodes.FindAsync(id);

            if (promoCode == null)
            {
                return NotFound();
            }

            return promoCode;
        }

        // POST: api/PromoCode
        [HttpPost]
        public async Task<ActionResult<PromoCode>> PostPromoCode(PromoCode promoCode)
        {
            _context.PromoCodes.Add(promoCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPromoCode", new { id = promoCode.PromoCodeId }, promoCode);
        }

        // PUT: api/PromoCode/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromoCode(int id, PromoCode promoCode)
        {
            if (id != promoCode.PromoCodeId)
            {
                return BadRequest();
            }

            _context.Entry(promoCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromoCodeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PromoCodeExists(int id)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/PromoCode/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PromoCode>> DeletePromoCode(int id)
        {
            var promoCode = await _context.PromoCodes.FindAsync(id);
            if (promoCode == null)
            {
                return NotFound();
            }

            _context.PromoCodes.Remove(promoCode);
            await _context.SaveChangesAsync();

            return promoCode;
        }

    }
}
