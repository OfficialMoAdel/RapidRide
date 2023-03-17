
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace RapidRide.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public BookingController(RapidRideDbContext context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }
        [HttpGet("getbyquery")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking([FromQuery] int? tripId, [FromQuery] int? bookingId, [FromQuery] DateTime? bookingTime, [FromQuery] string? bookingLocation)
        {
            var bookings = _context.Bookings.AsQueryable();

            if (tripId.HasValue)
            {
                bookings = bookings.Where(b => b.TripId == tripId.Value);
            }

            if (bookingId.HasValue)
            {
                bookings = bookings.Where(b => b.BookingId == bookingId.Value);
            }

            if (bookingTime.HasValue)
            {
                bookings = bookings.Where(b => b.BookingTime == bookingTime.Value);
            }

            if (!string.IsNullOrEmpty(bookingLocation))
            {
                bookings = bookings.Where(b => b.BookingLocation == bookingLocation);
            }

            return await bookings.ToListAsync();
        }



        // POST: api/Booking
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.BookingId }, booking);
        }

        // PUT: api/Booking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
