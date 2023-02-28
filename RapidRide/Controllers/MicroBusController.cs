using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace MoveEasyV2.Controllers
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
