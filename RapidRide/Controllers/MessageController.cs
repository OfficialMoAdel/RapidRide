using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace MoveEasyV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public MessageController(RapidRideDbContext context)
        {
            _context = context;
        }

        // GET: api/Message
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Message/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.MessageId)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Message
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = message.MessageId }, message);
        }

        // DELETE: api/Message/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Message>> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return message;
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
