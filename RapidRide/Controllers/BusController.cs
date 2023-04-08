using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace RapidRide.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BusController : ControllerBase
    {

        private readonly RapidRideDbContext _context;
        private readonly IWebHostEnvironment _env;


        public BusController(RapidRideDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Bus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bus>>> GetBuses()
        {
            return await _context.Buses.ToListAsync();
        }

        // GET: api/Bus/5  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBus(int id)
        {
            var bus = await _context.Buses.FindAsync(id);

            if (bus == null)
            {
                return NotFound();
            }

            return Ok(bus);
        }


        [HttpGet("getbyquery")]

        public ActionResult<IEnumerable<Bus>> GetBus([FromQuery] int? bookingId, [FromQuery] int? tripId, [FromQuery] int? busId, [FromQuery] bool? isActive, [FromQuery] string? city, [FromQuery] string? station)
        {
            var buses = _context.Buses.Include(b => b.Trips).Include(b => b.Bookings).AsQueryable();

            if (bookingId.HasValue)
            {
                buses = buses.Where(b => b.Bookings.Any(bo => bo.BookingId == bookingId.Value));
            }

            if (tripId.HasValue)
            {
                buses = buses.Where(b => b.Trips.Any(t => t.TripId == tripId.Value));
            }

            if (busId.HasValue)
            {
                buses = buses.Where(b => b.BusId == busId.Value);
            }

            if (isActive.HasValue)
            {
                buses = buses.Where(b => b.IsActive == isActive.Value);
            }

            if (!string.IsNullOrEmpty(city))
            {
                buses = buses.Where(b => b.City == city);
            }

            if (!string.IsNullOrEmpty(station))
            {
                buses = buses.Where(b => b.station == station);
            }

            return Ok(buses.ToList());
        }

        // PUT: api/Bus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBus(int id, Bus bus)
        {
            if (id != bus.BusId)
            {
                return BadRequest();
            }

            _context.Entry(bus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusExists(id))
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

        // POST: api/Bus
        [HttpPost]
        public async Task<ActionResult<Bus>> PostBus(Bus bus)
        {
            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBus", new { id = bus.BusId }, bus);
        }

        // DELETE: api/Bus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bus>> DeleteBus(int id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null)
            {
                return NotFound();
            }

            _context.Buses.Remove(bus);
            await _context.SaveChangesAsync();

            return bus;
        }

        [HttpPost("UploadProfilePicture/{busId}")]
        public async Task<IActionResult> UploadBusPicture(int busId, IFormFile file)
        {
            var bus = await _context.Buses.FindAsync(busId);
            if (bus == null)
            {
                return NotFound();
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("File not provided.");
            }

            // Save the image to a folder named "BusPictures"
            var folderPath = Path.Combine(_env.WebRootPath, "ProfilePictures");
            Directory.CreateDirectory(folderPath);

            var fileName = "bus_" + busId.ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            // Update the bus's profile picture URL
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}";
            var relativePath = $"/ProfilePictures/{fileName}";
            bus.ProfilePicture = $"{baseUrl}{relativePath}";

            _context.Entry(bus).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(bus);
        }

        private bool BusExists(int id)
        {
            return _context.Buses.Any(e => e.BusId == id);
        }
    }

}
