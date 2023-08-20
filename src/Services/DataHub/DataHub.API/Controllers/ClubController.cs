using DataHub.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly ILogger<ClubController> _logger;

        public ClubController(
            ILogger<ClubController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult> GetResult()
        {
            _logger.LogError(new Exception(), "erorr");
            return Ok();
        }
        [HttpGet("{query:string}")]
        public async Task<ActionResult<IEnumerable<ClubDetails>>> GetClubByName([FromRoute] string query)
        {
            _logger.LogError(new Exception(), "erorr");
            return null;
        }

    }
}
