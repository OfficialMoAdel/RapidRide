using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace RapidRide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MicroBusController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public MicroBusController(RapidRideDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MicroBus>>> GetMicroBuses()
        {
            return await _context.MicroBuses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MicroBus>> GetMicroBus(int id)
        {
            var microBus = await _context.MicroBuses.FindAsync(id);

            if (microBus == null)
            {
                return NotFound();
            }

            return microBus;
        }

        [HttpGet("getbyquery")]

        public ActionResult<IEnumerable<MicroBus>> GetMicroBus([FromQuery] int? tripId, [FromQuery] int? microBusId, [FromQuery] bool? isActive, [FromQuery] string? city, [FromQuery] string? station)
        {
            var microBusesQuery = _context.MicroBuses.Include(b => b.Trips).AsQueryable();
            if (tripId.HasValue)
            {
                microBusesQuery = microBusesQuery.Where(mb => mb.Trips.Any(t => t.TripId == tripId.Value));
            }

            if (microBusId.HasValue)
            {
                microBusesQuery = microBusesQuery.Where(mb => mb.MicroBusId == microBusId.Value);
            }

            if (isActive.HasValue)
            {
                microBusesQuery = microBusesQuery.Where(mb => mb.IsActive == isActive.Value);
            }

            if (!string.IsNullOrEmpty(city))
            {
                microBusesQuery = microBusesQuery.Where(mb => mb.City == city);
            }

            if (!string.IsNullOrEmpty(station))
            {
                microBusesQuery = microBusesQuery.Where(mb => mb.station == station);
            }

            var microBuses = microBusesQuery.ToList();

            if (microBuses == null || microBuses.Count == 0)
            {
                return NotFound();
            }

            return microBuses;
        }


        [HttpPost]
        public async Task<ActionResult<MicroBus>> CreateMicroBus(MicroBus microBus)
        {
            _context.MicroBuses.Add(microBus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMicroBus), new { id = microBus.MicroBusId }, microBus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMicroBus(int id, MicroBus microBus)
        {
            if (id != microBus.MicroBusId)
            {
                return BadRequest();
            }

            _context.Entry(microBus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MicroBusExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMicroBus(int id)
        {
            var microBus = await _context.MicroBuses.FindAsync(id);
            if (microBus == null)
            {
                return NotFound();
            }

            _context.MicroBuses.Remove(microBus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MicroBusExists(int id)
        {
            return _context.MicroBuses.Any(e => e.MicroBusId == id);
        }
    }
}
