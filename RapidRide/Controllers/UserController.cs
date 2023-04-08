using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RapidRide.Entities;
using static RapidRide.Entities.User;
using RapidRide.Service;

namespace RapidRide.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RapidRideDbContext _context;
        private readonly AuthService _authService;
        private readonly IWebHostEnvironment _env;


        public UserController(RapidRideDbContext context, AuthService authService, IWebHostEnvironment env)
        {
            _context = context;
            _authService = authService;
            _env = env;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("getbyquery")]
        public ActionResult<IEnumerable<User>> Get([FromQuery] bool? isActive, [FromQuery] int? tripId, [FromQuery] int? bookingId, [FromQuery] int? messageId, [FromQuery] int? walletId)
        {
            var usersQuery = _context.Users.Include(u => u.Trips).Include(u => u.Bookings).Include(u => u.Messages).Include(u => u.Wallet).AsQueryable();

            if (isActive.HasValue)
            {
                usersQuery = usersQuery.Where(u => u.IsActive == isActive.Value);
            }

            if (tripId.HasValue)
            {
                usersQuery = usersQuery.Where(u => u.Trips.Any(t => t.TripId == tripId.Value));
            }

            if (bookingId.HasValue)
            {
                usersQuery = usersQuery.Where(u => u.Bookings.Any(b => b.BookingId == bookingId.Value));
            }

            if (messageId.HasValue)
            {
                usersQuery = usersQuery.Where(u => u.Messages.Any(m => m.MessageId == messageId.Value));
            }

            if (walletId.HasValue)
            {
                usersQuery = usersQuery.Where(u => u.WalletId == walletId.Value);
            }

            var users = usersQuery.ToList();

            if (users == null || users.Count == 0)
            {
                return NotFound();
            }

            return users;
        }



        // POST: api/User
        [HttpPost]
        public IActionResult PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationModel model)
        {
            var user = _authService.Register(model.Email, model.NationalId, model.Password);
            return Ok(user);
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel model)
        {
            var token = _authService.Login(model.Login, model.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [HttpPost("UploadProfilePicture/{userId}")]
        public async Task<IActionResult> UploadProfilePicture(int userId, IFormFile file)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("File not provided.");
            }

            // Save the image to a folder named "ProfilePictures"
            var folderPath = Path.Combine(_env.WebRootPath, "ProfilePictures");
            Directory.CreateDirectory(folderPath);

            var fileName = "user_" + userId.ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            // Update the user's profile picture URL
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}";
            var relativePath = $"/ProfilePictures/{fileName}";
            user.ProfilePicture = $"{baseUrl}{relativePath}";

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(user);
        }


    }

}

