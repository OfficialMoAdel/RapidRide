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
        //api/buses?bookingId=123&tripId=456&isActive=true&city=New%20York&station=Central%20Station

        public ActionResult<IEnumerable<Bus>> GetBuses([FromQuery] int? bookingId, [FromQuery] int? tripId, [FromQuery] int? busId, [FromQuery] bool? isActive, [FromQuery] string? city, [FromQuery] string? station)
        {
            var busesQuery = _context.Buses.Include(b => b.Trips).Include(b => b.Bookings).AsQueryable();

            if (bookingId.HasValue)
            {
                busesQuery = busesQuery.Where(b => b.Bookings.Any(bo => bo.BookingId == bookingId.Value));
            }

            if (tripId.HasValue)
            {
                busesQuery = busesQuery.Where(b => b.Trips.Any(t => t.TripId == tripId.Value));
            }

            if (busId.HasValue)
            {
                busesQuery = busesQuery.Where(b => b.BusId == busId.Value);
            }

            if (isActive.HasValue)
            {
                busesQuery = busesQuery.Where(b => b.IsActive == isActive.Value);
            }

            if (!string.IsNullOrEmpty(city))
            {
                busesQuery = busesQuery.Where(b => b.City == city);
            }

            if (!string.IsNullOrEmpty(station))
            {
                busesQuery = busesQuery.Where(b => b.station == station);
            }

            var buses = busesQuery.ToList();

            if (buses == null || buses.Count == 0)
            {
                return NotFound();
            }

            return buses;
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
