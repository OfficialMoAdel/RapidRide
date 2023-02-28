using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide;

namespace MoveEasyV2.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public PaymentController(RapidRideDbContext context)
        {
            _context = context;
        }

        // GET: api/Payment
        [HttpGet]
        public ActionResult<IEnumerable<Payment>> GetPayments()
        {
            return _context.Payments;
        }

        // GET: api/Payment/5
        [HttpGet("{id}")]
        public ActionResult<Payment> GetPayment(int id)
        {
            var payment = _context.Payments.Find(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // POST: api/Payment
        [HttpPost]
        public ActionResult<Payment> PostPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();

            return CreatedAtAction("GetPayment", new { id = payment.PaymentId }, payment);
        }

        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public ActionResult PutPayment(int id, Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public ActionResult<Payment> DeletePayment(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            _context.SaveChanges();

            return payment;
        }
    }
}

