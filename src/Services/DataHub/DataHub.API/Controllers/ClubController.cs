using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        // GET: api/<ClubController>
        [HttpGet(Name = "GetClubByName")]
        public IEnumerable<string> GetClubByName([FromQuery] string query)
        {
            return new string[] { "value1", "value2" };
        }

    }
}
