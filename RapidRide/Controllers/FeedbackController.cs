﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace RapidRide.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public FeedbackController(RapidRideDbContext context)
        {
            _context = context;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedback()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        // GET: api/Feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }
        [HttpGet("getbyquery")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedback([FromQuery] int? tripId, [FromQuery] int? feedbackId, [FromQuery] int? rating, [FromQuery] string? comment)
        {
            var feedbacks = _context.Feedbacks.AsQueryable();

            if (tripId.HasValue)
            {
                feedbacks = feedbacks.Where(f => f.TripId == tripId.Value);
            }

            if (feedbackId.HasValue)
            {
                feedbacks = feedbacks.Where(f => f.FeedbackId == feedbackId.Value);
            }

            if (rating.HasValue)
            {
                feedbacks = feedbacks.Where(f => f.Rating == rating.Value);
            }

            if (!string.IsNullOrEmpty(comment))
            {
                feedbacks = feedbacks.Where(f => f.Comment.Contains(comment));
            }

            var result = await feedbacks.ToListAsync();

            if (result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }


        // PUT: api/Feedback/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, Feedback feedback)
        {
            if (id != feedback.FeedbackId)
            {
                return BadRequest();
            }

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
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

        // POST: api/Feedback
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new { id = feedback.FeedbackId }, feedback);
        }

        // DELETE: api/Feedback/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Feedback>> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.FeedbackId == id);
        }
    }

}
