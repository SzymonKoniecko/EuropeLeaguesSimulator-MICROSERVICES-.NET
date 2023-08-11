using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProcessManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        // GET: api/<ProcessController>
        [HttpGet]
        public IEnumerable<string> GetTeamById([FromQuery] int id)
        {
            return new string[] { "value1", "value2" };
        }

    }
}
