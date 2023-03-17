using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace RapidRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public CarController(RapidRideDbContext context)
        {
            _context = context;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // Other controller actions (e.g. GetCarById, CreateCar, UpdateCar, DeleteCar) can be added here
        // GET: api/Car/5//   //GET api/Car?bookingId=1&tripId=2&messageId=3&isActive=true&city=NewYork&carId=123
        [HttpGet("getbyquery")]
        public ActionResult<IEnumerable<Car>> GetCar([FromQuery] int? bookingId, [FromQuery] int? tripId, [FromQuery] int? messageId, [FromQuery] bool? isActive, [FromQuery] string? city, [FromQuery] int? carId)
        {
            var cars = _context.Cars.Include(c => c.Trips).Include(c => c.Messages).Include(c => c.Bookings).AsQueryable();

            if (bookingId.HasValue)
            {
                cars = cars.Where(c => c.Bookings.Any(b => b.BookingId == bookingId.Value));
            }

            if (tripId.HasValue)
            {
                cars = cars.Where(c => c.Trips.Any(t => t.TripId == tripId.Value));
            }

            if (messageId.HasValue)
            {
                cars = cars.Where(c => c.Messages.Any(m => m.MessageId == messageId.Value));
            }

            if (isActive.HasValue)
            {
                cars = cars.Where(c => c.IsActive == isActive.Value);
            }

            if (!string.IsNullOrEmpty(city))
            {
                cars = cars.Where(c => c.City == city);
            }

            if (carId.HasValue)
            {
                cars = cars.Where(c => c.CarId == carId.Value);
            }

            return Ok(cars.ToList());
        }


        // PUT: api/Car/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Car
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.CarId }, car);
        }

        // DELETE: api/Car/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return car;
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }
    }

}
