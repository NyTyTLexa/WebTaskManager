using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            const string KEY = "LANVER2024@";
            private readonly IConfiguration _configuration;

        private DataContext _appDbContext;

        public AuthController(DataContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [SwaggerOperation("Login")]
        public async Task<IActionResult> Login([FromBody] Users users)
        {
            if (ModelState.IsValid)
            {
                var user = await AuthenticateUser(users.Login, users.Password);
                if (user != null)
                {
                    var token = GenerateJwtToken(user);
                    return Ok(new { token });
                }
            }
            return Unauthorized();
        }

        [HttpPost("register")]
        [SwaggerOperation("reg")]
        public async Task<IActionResult> Register([FromBody] Users users)
        {
            if (ModelState.IsValid)
            {
                var user = await CreateUser(users);
                if (user != null)
                {
                    var token = GenerateJwtToken(user);
                    return Ok(new { token });
                }
            }
            return BadRequest();
        }
     

        private async Task<Users> AuthenticateUser(string login, string password)
            { 
            var user = _appDbContext.USER.FirstOrDefault(a=> a.Login == login && a.Password == password);
            if (user != null)
            {
                return user;
            }
                return null!;
            }

            private async Task<Users> CreateUser(Users user)
            {
            var users = _appDbContext.USER.ToList();
                if (users.Any(a=>a.Login==user.Login))
                {
                    return null!;
                }
                else
                {
                   user.Id = Guid.NewGuid().ToString();
                _appDbContext.USER.Add(user);
                _appDbContext.SaveChanges();
                    return user;
                }
            }

            private string GenerateJwtToken(Users user)
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, user.Login),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
            var key = new SymmetricSecurityKey(new byte[32]);
                Encoding.UTF8.GetBytes(KEY);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
