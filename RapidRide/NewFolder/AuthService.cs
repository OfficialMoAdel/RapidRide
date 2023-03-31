using Microsoft.IdentityModel.Tokens;
using RapidRide.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RapidRide.NewFolder
{

    public class AuthService
    {
        private readonly RapidRideDbContext _context;

        public AuthService(RapidRideDbContext context)
        {
            _context = context;
        }

        public User Register(string email, int nationalId, string password)
        {
            // Create password hash and salt
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Create new user with the provided information
            var user = new User
            {
                Email = email,
                NationalId = nationalId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            // Add the user to the database and save changes
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public Token Login(string login, string password)
        {
            // Retrieve the user by email or phone number
            var user = _context.Users.SingleOrDefault(u => u.Email == login /*|| u.PhoneNumber == login*/);

            if (user == null || !VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                // Return null if the user is not found or the password is incorrect
                return null;
            }

            // Create a new JWT token and store it in the database
            var token = new Token
            {
                UserId = user.UserId,
                JwtToken = GenerateJwtToken(user),
                ExpiresAt = DateTime.UtcNow.AddDays(7), // Set the token to expire in 7 days
                User = user
            };

            _context.Tokens.Add(token);
            _context.SaveChanges();

            return token;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return !computedHash.Where((t, i) => t != storedHash[i]).Any();
            }
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("QFdpG9q6+Qvw4FzzZTUFJGIvi8ZNNe4ocy9Z5i6aJfE="));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
              new Claim(ClaimTypes.Email, user.Email),
             //new Claim(ClaimTypes.Name, user.Name)
                // Add any additional claims as needed
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}

