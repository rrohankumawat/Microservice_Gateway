using AuthService.Data;
using AuthService.Dto;
using AuthService.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AppDbContext _context) : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e652a7190c582a43ca5f37b10363321d8d741edb433cf2e283765ce5e1c17830"));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: "rohan_issuer",
                audience: "rohan_audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds

                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        [HttpPost("Register")]
        public IActionResult Register(RegisterDto request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("Email already exists.");
            }
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User registered successfully.");
        }
    }
}
