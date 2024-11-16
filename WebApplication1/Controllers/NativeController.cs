using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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

        public AuthController(DataContext AppDbContext, IConfiguration configuration)
        {
            _appDbContext = AppDbContext;
            _configuration = configuration;
        }


        [HttpGet("GetStatus")]
        [SwaggerOperation("Status")]
        public async Task<IActionResult> Status()
        {
            if (ModelState.IsValid)
            {
                var tasks = GetStatus();
                if (tasks != null)
                {
                    var token = tasks;
                    return Ok(new { token });
                }
            }
            return Unauthorized();
        }

        private List<Model.Status> GetStatus()
        {
           return _appDbContext.Status.ToList();
        }

        [HttpGet("GetPriority")]
        [SwaggerOperation("Priority")]
        public async Task<IActionResult> Priority()
        {
            if (ModelState.IsValid)
            {
                var tasks = GetPriority();
                if (tasks != null)
                {
                    var token = tasks;
                    return Ok(new { token });
                }
            }
            return Unauthorized();
        }

        private List<Model.Priority> GetPriority()
        {
            return _appDbContext.Priority.ToList();
        }

        [HttpGet("GetUserTask")]
            [SwaggerOperation("Task")]
            public async Task<IActionResult> GetTaskUser([FromQuery] [Required]string id)
            {
                if (ModelState.IsValid)
                {
                    var tasks = GetTask(id);
                    if (tasks != null)
                    {
                        var token = tasks;
                        return Ok(new { token });
                    }
                }
                return Unauthorized();
            }

        [HttpPost("login")]
            [SwaggerOperation("Login")]
            public async Task<IActionResult> Login([FromBody] User model)
            {
                if (ModelState.IsValid)
                {
                    var user = await AuthenticateUser(model.Login, model.Password);
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
            public async Task<IActionResult> Register([FromBody] User model)
            {
                if (ModelState.IsValid)
                {
                    var user = await CreateUser(model);
                    if (user != null)
                    {
                        var token = GenerateJwtToken(user);
                        return Ok(new { token });
                    }
                }
                return BadRequest();
            }
        private DataContext _context;
        private  List<Model.Task> GetTask(string id)
        {
            var userAndTasks = _appDbContext.UserandTask.Where(a=>a.UserId==id).ToList();
            var tasks = new List<Model.Task>();
            var task123 = _appDbContext.Task.ToList();
            var status = _appDbContext.Status.ToList();
            var priority = _appDbContext.Priority.ToList();
            foreach (var uatask in userAndTasks)
            {
                var task = _appDbContext.Task.First(a => a.id == uatask.TaskId);
                tasks.Add(task);
            }
           
            return tasks;
        }



        private async Task<Model.User> AuthenticateUser(string login, string password)
            { 
            var user = _appDbContext.USER.FirstOrDefault(a=>a.Login == login&&a.Password==password);
            if (user != null)
            {
                return user;
            }
                return null;
            }

            private async Task<User> CreateUser(User user)
            {
            var users = _appDbContext.USER.ToList();
                if (users.Any(a=>a.Login==user.Login))
                {
                    return null;
                }
                else
                {
                   user.Id = Guid.NewGuid().ToString();
                _appDbContext.USER.Add(user);
                _appDbContext.SaveChanges();
                    return user;
                }
            }

            private string GenerateJwtToken(User user)
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
