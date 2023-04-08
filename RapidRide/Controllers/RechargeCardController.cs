using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidRide.Entities;
using RapidRide.Service;
using System.Web;

namespace RapidRide.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RechargeCardController : ControllerBase
    {
        private readonly RechargeCardService _rechargeCardService;

        private readonly RapidRideDbContext _context;

        public RechargeCardController(RapidRideDbContext context, RechargeCardService rechargeCardService)
        {
            _context = context;
            _rechargeCardService = rechargeCardService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RechargeCard>>> GetRechargeCards()
        {
            return await _context.RechargeCards.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RechargeCard>> GetRechargeCard(int id)
        {
            var rechargeCard = await _context.RechargeCards.FindAsync(id);

            if (rechargeCard == null)
            {
                return NotFound();
            }

            return rechargeCard;
        }

        ///api/rechargecards?category=Prepaid&dateFrom=2023-01-01&dateTo=2023-03-31
        [HttpGet]
        [Route("api/rechargecards")]
        public ActionResult GetRechargeCardsByQuery(string data)
        {
            // Parse the query data into a dictionary
            var queryData = HttpUtility.ParseQueryString(data);

            // Extract the filter criteria
            var category = queryData["category"];
            var dateFrom = DateTime.Parse(queryData["dateFrom"]);
            var dateTo = DateTime.Parse(queryData["dateTo"]);

            // Query the database for recharge cards matching the filter criteria
            var rechargeCards = _context.RechargeCards
                .Where(rc => rc.Category == category && rc.Date >= dateFrom && rc.Date <= dateTo)
                .ToList();

            // Return the recharge cards as a response
            return Ok(rechargeCards);
        }


        [HttpGet("{number}")]
        public IActionResult GetByNumber(string number)
        {
            var card = _context.RechargeCards.SingleOrDefault(c => c.Number == number);
            if (card == null)
            {
                return NotFound();
            }

            if (!card.IsActive)
            {
                // the card has already been charged, so return a 400 Bad Request response
                return BadRequest("The card has already been charged");
            }

            // mark the card as inactive and save changes to the database
            card.IsActive = false;
            _context.SaveChanges();

            return Ok("Congratulations, the card is valid!");
        }


        [HttpPost]
        [Route("api/rechargecards/generate")]
        public async Task<IActionResult> GenerateRechargeCards(int count, string category)
        {
            if (count <= 0 || string.IsNullOrWhiteSpace(category))
            {
                return BadRequest("Invalid input parameters.");
            }

            var rechargeCards = await _rechargeCardService.GenerateRechargeCards(count, category);

            // Generate the Excel file
            var excelBytes = ExcelGenerator.GenerateRechargeCardsInvoiceExcel(rechargeCards);

            // Return the Excel file as a file download
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RechargeCardsInvoice.xlsx");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRechargeCard(int id)
        {
            var rechargeCard = await _context.RechargeCards.FindAsync(id);

            if (rechargeCard == null)
            {
                return NotFound();
            }

            _context.RechargeCards.Remove(rechargeCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}