using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace RapidRide.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public TripController(RapidRideDbContext context)
        {
            _context = context;
        }

        // GET: api/Trip
        [HttpGet]
        public IEnumerable<Trip> GetTrips()
        {
            return _context.Trips;
        }

        // GET: api/Trip/5
        //[HttpGet("{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrip([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trip = await _context.Trips.FindAsync(id);

            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Trip>> GetTrips([FromQuery] int? id, [FromQuery] int? userId, [FromQuery] DateTime? pickupTime, [FromQuery] float? distance, [FromQuery] float? cost, [FromQuery] int? fareId, [FromQuery] int? bookingId, [FromQuery] int? busId, [FromQuery] int? feedbackId)
        {
            var trips = _context.Trips.Include(t => t.Fare).Include(t => t.Booking).Include(t => t.Bus).Include(t => t.Feedbacks).AsQueryable();

            if (id.HasValue)
            {
                trips = trips.Where(t => t.TripId == id.Value);
            }

            if (userId.HasValue)
            {
                trips = trips.Where(t => t.UserId == userId.Value);
            }

            if (pickupTime.HasValue)
            {
                trips = trips.Where(t => t.PickupTime == pickupTime.Value);
            }

            if (distance.HasValue)
            {
                trips = trips.Where(t => t.Distance == distance.Value);
            }

            if (cost.HasValue)
            {
                trips = trips.Where(t => t.Cost == cost.Value);
            }

            if (fareId.HasValue)
            {
                trips = trips.Where(t => t.FareId == fareId.Value);
            }

            if (bookingId.HasValue)
            {
                trips = trips.Where(t => t.BookingId == bookingId.Value);
            }

            if (busId.HasValue)
            {
                trips = trips.Where(t => t.BusId == busId.Value);
            }

            if (feedbackId.HasValue)
            {
                trips = trips.Where(t => t.Feedbacks.Any(f => f.FeedbackId == feedbackId.Value));
            }

            return Ok(trips.ToList());
        }



        // PUT: api/Trip/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrip([FromRoute] int id, [FromBody] Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trip.TripId)
            {
                return BadRequest();
            }

            _context.Entry(trip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
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

        // POST: api/Trip
        [HttpPost]
        public async Task<IActionResult> PostTrip([FromBody] Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrip", new { id = trip.TripId }, trip);
        }

        // DELETE: api/Trip/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrip([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }

            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();

            return Ok(trip);
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.TripId == id);
        }
    }
}

