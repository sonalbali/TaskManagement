using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Task_Managment_System.Data;
using Task_Managment_System.Model.Entities;
using Task_Managment_System.Migrations;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace Task_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {

        private readonly TaskDBContext _context;

        public RegistrationController(TaskDBContext context)
        {
            _context = context;
        }

        [HttpPost("register")]    
        public IActionResult Register([FromBody] Registration user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return BadRequest("User already exists.");
            }

            user.PasswordHash = HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public IActionResult Login(Registration user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (dbUser == null || !VerifyPassword(dbUser.PasswordHash, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = GenerateJwtToken(user.Username,user.UserId);
            return Ok(new { token, username = dbUser.Username,
        userId = dbUser.UserId  });
        }

        private string GenerateJwtToken(string username,int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TEeTC2vsKGXoi8ZrbBdnrbkPODv9lG0ss3716kSvNO0=\""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
             new Claim("UserId", UserId.ToString()),  // Add UserId claim
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: "http://localhost",
                audience: "http://localhost",
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            byte[] salt = Encoding.UTF8.GetBytes("SomeRandomSaltValue");
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        private bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            return HashPassword(providedPassword) == hashedPassword;
        }

        

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<Registration>>> GetUsers()
        {         
            var users = await _context.Users
                .Select(u => new { u.UserId, u.Name }) // Only send necessary fields
                .ToListAsync();

            return Ok(users);
        
        }
    }



}
