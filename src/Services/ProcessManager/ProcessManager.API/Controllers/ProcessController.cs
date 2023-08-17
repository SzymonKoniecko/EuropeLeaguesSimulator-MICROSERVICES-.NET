using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProcessManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly ILogger<ProcessController> _logger;

        public ProcessController(ILogger<ProcessController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<string> GetClubsById([FromQuery] string query)
        {

            _logger.LogInformation($"Search process started for query = {query}");
            return new string[] { "value1", "value2" };
        }

    }
}
