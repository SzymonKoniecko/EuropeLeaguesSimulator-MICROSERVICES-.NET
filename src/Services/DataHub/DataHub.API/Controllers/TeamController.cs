using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        // GET: api/<TeamController>
        [HttpGet]
        public IEnumerable<string> Get([FromQuery] int id)
        {
            return new string[] { id.ToString() };
        }
    }
}
