using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Controllers;
using WebApplication1.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {

        const string KEY = "LANVER2024@";
        private readonly IConfiguration _configuration;

        private DataContext _appDbContext;

        public TasksController(DataContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }
        [HttpGet()]
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

        [HttpPost()]
        [SwaggerOperation("AddTask")]
        public async Task<IActionResult> AddTask(UserAndTask userAndTask)
        {
            if (ModelState.IsValid)
            {
                var tasks = PostUserTask(userAndTask);
                if (tasks != null)
                {
                    var token = tasks;
                    return Ok(new { token });
                }
            }
            return Unauthorized();
        }

        [HttpPut()]
        [SwaggerOperation("EditTask")]
        public async Task<IActionResult> EditTask(UserAndTask userAndTask)
        {
            if (ModelState.IsValid)
            {
                var tasks = PutUserTask(userAndTask);
                if (tasks)
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }

        [HttpDelete()]
        [SwaggerOperation("DeleteTask")]
        public async Task<IActionResult> DeleteTask(UserAndTask userAndTask)
        {
            if (ModelState.IsValid)
            {
                var tasks = DelUserTask(userAndTask);
                if (tasks)
                {
                    return Ok();
                }
            }
            return Unauthorized();
        }

        private List<WebApplication1.Model.Task> GetTask(string id)
        {
            var userAndTasks = _appDbContext.UserandTask.Where(a => a.Userid == id).ToList();
            var tasks = new List<WebApplication1.Model.Task>();
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

        private bool PostUserTask(UserAndTask userAndTask)
        {
            var task = userAndTask.task;
            var user = userAndTask.user;
            if (task != null)
            {
                task.id = _appDbContext.Task.Max(a => a.id) + 1;
                var tasks = new UserAndTask();
                tasks.Userid = user.Id;
                tasks.TaskId = task.id;
                tasks.id = _appDbContext.UserandTask.Max(a => a.id) + 1;
                _appDbContext.Task.Add(task);
                _appDbContext.SaveChanges();
                _appDbContext.UserandTask.Add(tasks);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PutUserTask(UserAndTask userAndTask)
        {
            var task = userAndTask.task;
            var user = userAndTask.user;
            if (task != null)
            {
                _appDbContext.Task.Update(task);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DelUserTask(UserAndTask userandTask)
        {
            var task = _appDbContext.Task.First(a => a.id == userandTask.TaskId);
            var user = _appDbContext.USER.First(a => a.Id == userandTask.Userid);
            if (task != null)
            {
                _appDbContext.Task.Remove(task);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
