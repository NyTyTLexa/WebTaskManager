using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Controllers;
using WebApplication1.Model;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PriorityController : ControllerBase
    {

        const string KEY = "LANVER2024@";
        private readonly IConfiguration _configuration;

        private DataContext _appDbContext;

        public PriorityController(DataContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }
        [HttpGet()]
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

        [HttpPost()]
        [SwaggerOperation("AddPriority")]
        public async Task<IActionResult> AddPriority([FromQuery][Required] Priority priority)
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

        [HttpPut()]
        [SwaggerOperation("EditPriority")]
        public async Task<IActionResult> EditPriority([FromQuery][Required] Priority priority)
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

        [HttpDelete()]
        [SwaggerOperation("DeletePriority")]
        public async Task<IActionResult> DeletePriority([FromQuery][Required] Priority priority)
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

        private List<Priority> GetPriority()
        {
            return _appDbContext.Priority.ToList();
        }

        private bool PostPriority(Priority priority)
        {
            if (priority == null)
            {
                priority!.Id = _appDbContext.Priority.Max(a => a.Id) + 1;
                _appDbContext.Priority.Add(priority);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PutPriority(Priority priority)
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

        private bool DelPriority(Priority priority)
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
    }
}
