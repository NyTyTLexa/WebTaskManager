using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Controllers;
using WebApplication1.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController: ControllerBase
    {
        const string KEY = "LANVER2024@";
        private readonly IConfiguration _configuration;

        private DataContext _appDbContext;

        public StatusController(DataContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        [HttpGet()]
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

        [HttpPost()]
        [SwaggerOperation("AddStatus")]
        public async Task<IActionResult> AddStatus([FromQuery][Required] Status status)
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

        [HttpPut()]
        [SwaggerOperation("EditStatus")]
        public async Task<IActionResult> EditStatus([FromQuery][Required] Status status)
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

        [HttpDelete()]
        [SwaggerOperation("DeleteStatus")]
        public async Task<IActionResult> DeleteStatus([FromQuery][Required] Status status)
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

        private List<Status> GetStatus()
        {
            return _appDbContext.Status.ToList();
        }

        private bool PostStatus(Status status)
        {
            if (status != null)
            {
                status.Id = _appDbContext.Status.Max(a => a.Id) + 1;
                _appDbContext.Status.Add(status!);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool PutStatus(Status status)
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

        private bool DelStatus(Status status)
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
    }
}
