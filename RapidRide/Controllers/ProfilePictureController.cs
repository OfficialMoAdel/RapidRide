using Microsoft.AspNetCore.Mvc;
using RapidRide;

namespace MoveEasyV2.Controllers
{
    [ApiController]
    public class ProfilePictureController : ControllerBase
    {
        private readonly RapidRideDbContext _context;

        public ProfilePictureController(RapidRideDbContext context)
        {
            _context = context;
        }

        [HttpPost("users/{userId}/profilepicture")]
        public async Task<IActionResult> UploadProfilePicture(int userId, IFormFile file)
        {
            // Retrieve the user by their ID
            var user = await _context.Users.FindAsync(userId);

            // Check if the user exists
            if (user == null)
            {
                return NotFound();
            }

            // Check if a file was uploaded
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please upload a file");
            }

            // Check if the file is an image
            if (!file.ContentType.StartsWith("image/"))
            {
                return BadRequest("Please upload an image file");
            }

            // Convert the file to a byte array
            byte[] profilePicture;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                profilePicture = stream.ToArray();
            }

            // Save the profile picture to the database
            user.ProfilePicture = profilePicture;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("users/{userId}/profilepicture")]
        public async Task<IActionResult> DownloadProfilePicture(int userId)
        {
            // Retrieve the user by their ID
            var user = await _context.Users.FindAsync(userId);

            // Check if the user exists
            if (user == null)
            {
                return NotFound();
            }

            // Check if the user has a profile picture
            if (user.ProfilePicture == null || user.ProfilePicture.Length == 0)
            {
                return NotFound();
            }

            // Return the profile picture as a file
            return File(user.ProfilePicture, "image/jpeg");
        }

        [HttpPut("users/{userId}/profilepicture")]
        public async Task<IActionResult> UpdateProfilePicture(int userId, IFormFile file)
        {
            // Retrieve the user by their ID
            var user = await _context.Users.FindAsync(userId);

            // Check if the user exists
            if (user == null)
            {
                return NotFound();
            }

            // Check if a file was uploaded
            if (file == null || file.Length == 0)
            {
                return BadRequest("Please upload a file");
            }

            // Check if the file is an image
            if (!file.ContentType.StartsWith("image/"))
            {
                return BadRequest("Please upload an image file");
            }

            // Convert the file to a byte array
            byte[] profilePicture;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                profilePicture = stream.ToArray();
            }

            // Update the profile picture in the database
            user.ProfilePicture = profilePicture;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
