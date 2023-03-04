using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace RapidRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FareController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public FareController(RapidRideDbContext context)
        {
            _context = context;
        }

        // GET: api/Fare
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fare>>> GetFares()
        {
            return await _context.Fares.ToListAsync();
        }

        // GET: api/Fare/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fare>> GetFare(int id)
        {
            var fare = await _context.Fares.FindAsync(id);

            if (fare == null)
            {
                return NotFound();
            }

            return fare;
        }

        // PUT: api/Fare/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFare(int id, Fare fare)
        {
            if (id != fare.FareId)
            {
                return BadRequest();
            }

            _context.Entry(fare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FareExists(id))
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

        // POST: api/Fare
        [HttpPost]
        public async Task<ActionResult<Fare>> PostFare(Fare fare)
        {
            _context.Fares.Add(fare);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFare", new { id = fare.FareId }, fare);
        }

        // DELETE: api/Fare/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fare>> DeleteFare(int id)
        {
            var fare = await _context.Fares.FindAsync(id);
            if (fare == null)
            {
                return NotFound();
            }

            _context.Fares.Remove(fare);
            await _context.SaveChangesAsync();

            return fare;
        }

        private bool FareExists(int id)
        {
            return _context.Fares.Any(e => e.FareId == id);
        }
    }

}
