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

        [HttpPost("PostStatus")]
        [SwaggerOperation("AddStatus")]
        public async Task<IActionResult> AddStatus([FromQuery][Required] Model.Status status)
        {
            if (ModelState.IsValid)
            {
                var query = PostStatus(status);
                if (query)
                { 
                    return Ok();
                }
            }
            return Unauthorized();
        }

        [HttpPut("PutStatus")]
        [SwaggerOperation("EditStatus")]
        public async Task<IActionResult> EditStatus([FromQuery][Required] Model.Status status)
        {
            if (ModelState.IsValid)
            {
                var query = PutStatus(status);
                if (query)
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }

        [HttpDelete("DeleteStatus")]
        [SwaggerOperation("DeleteStatus")]
        public async Task<IActionResult> DeleteStatus([FromQuery][Required] Model.Status status)
        {
            if (ModelState.IsValid)
            {
                var query = DelStatus(status);
                if (query)
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }

        [HttpGet("GetUserTask")]
        [SwaggerOperation("Task")]
        public async Task<IActionResult> GetTaskUser([FromQuery][Required] string id)
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

        [HttpPost("PostTask")]
        [SwaggerOperation("AddTask")]
        public async Task<IActionResult> AddTask([FromQuery][Required] Model.Task task, [FromQuery][Required] Model.User user)
        {
            if (ModelState.IsValid)
            {
                var tasks = PostUserTask(task, user);
                if (tasks != null)
                {
                    var token = tasks;
                    return Ok(new { token });
                }
            }
            return Unauthorized();
        }

        [HttpPut("PutTask")]
        [SwaggerOperation("EditTask")]
        public async Task<IActionResult> EditTask([FromQuery][Required] Model.Task task, [FromQuery][Required] Model.User user)
        {
            if (ModelState.IsValid)
            {
                var tasks = PutUserTask(task,user);
                if (tasks)
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }

        [HttpDelete("DeleteTask")]
        [SwaggerOperation("DeleteTask")]
        public async Task<IActionResult> DeleteTask([FromQuery][Required] Model.Task task, [FromQuery][Required] Model.User user)
        {
            if (ModelState.IsValid)
            {
                var tasks = DelUserTask(task,user);
                if (tasks)
                {
                    return Ok();
                }
            }
            return Unauthorized();
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

        [HttpPost("PostPriority")]
        [SwaggerOperation("AddPriority")]
        public async Task<IActionResult> AddPriority([FromQuery][Required] Model.Priority priority)
        {
            if (ModelState.IsValid)
            {
                var tasks = PostPriority(priority);
                if (tasks)
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }

        [HttpPut("PutPriority")]
        [SwaggerOperation("EditPriority")]
        public async Task<IActionResult> EditPriority([FromQuery][Required] Model.Priority priority)
        {
            if (ModelState.IsValid)
            {
                var tasks = PutPriority(priority);
                if (tasks)
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }

        [HttpDelete("DeletePriority")]
        [SwaggerOperation("DeletePriority")]
        public async Task<IActionResult> DeletePriority([FromQuery][Required] Model.Priority priority)
        {
            if (ModelState.IsValid)
            {
                var tasks = DelPriority(priority);
                if (tasks)
                {
                    return Ok();
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

        private List<Model.Status> GetStatus()
        {
           return _appDbContext.Status.ToList();
        }

        private bool PostStatus(Model.Status status)
        {
            if (status != null)
            {
                status.id = _appDbContext.Status.Max(a => a.id) + 1;
                _appDbContext.Status.Add(status!);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PutStatus(Model.Status status)
        {
            if (status != null)
            {
                _appDbContext.Status.Update(status!);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DelStatus(Model.Status status)
        {

            if (status != null)
            {
                _appDbContext.Status.Remove(status!);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Model.Priority> GetPriority()
        {
            return _appDbContext.Priority.ToList();
        }

        private bool PostPriority(Model.Priority priority)
        {
            if (priority == null)
            {
                priority.id = _appDbContext.Priority.Max(a => a.id) + 1;
                _appDbContext.Priority.Add(priority);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PutPriority(Model.Priority priority)
        {
            if (priority == null)
            {
                _appDbContext.Priority.Update(priority!);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DelPriority(Model.Priority priority)
        {
            if (priority == null)
            {
                _appDbContext.Priority.Remove(priority!);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
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

        private bool PostUserTask(Model.Task task,Model.User user)
        {
          if(task == null)
            {
                var Tasks = new UserandTask();
                Tasks.UserId = user.Id;
                Tasks.TaskId = task.id;
                Tasks.Id = _appDbContext.UserandTask.Max(a=>a.Id) + 1;
                _appDbContext.Task.Add(task);
                _appDbContext.SaveChanges();
                _appDbContext.UserandTask.Add(Tasks);
                _appDbContext.SaveChanges();
                return true; 
            }
          else
            {
                return false;
            }
        }

        private bool PutUserTask(Model.Task task, Model.User user)
        {
            if (task == null)
            {
                var Tasks = new UserandTask();
                Tasks.UserId = user.Id;
                Tasks.TaskId = task.id;
                Tasks.Id = _appDbContext.UserandTask.First(a=>a.TaskId == task.id&&a.UserId == user.Id).Id;
                _appDbContext.Task.Update(task);
                _appDbContext.SaveChanges();
                _appDbContext.UserandTask.Update(Tasks);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DelUserTask(Model.Task task, Model.User user)
        {
            if (task == null)
            {
                var Tasks = new UserandTask();
                Tasks.UserId = user.Id;
                Tasks.TaskId = task.id;
                Tasks.Id = _appDbContext.UserandTask.First(a => a.TaskId == task.id && a.UserId == user.Id).Id;
                _appDbContext.Task.Remove(task);
                _appDbContext.SaveChanges();
                _appDbContext.UserandTask.Remove(Tasks);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
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
