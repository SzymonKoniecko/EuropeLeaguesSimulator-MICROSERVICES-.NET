using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(
            ILogger<LogController> logger)
        {
            _logger = logger;
        }
        // GET: api/<LogController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInformation("My Name is Mohammed Ahmed Hussien");
            _logger.LogWarning("Please, can you check your app's performance");
            _logger.LogError(new Exception(), "Booom, there is an exception");


            return new string[] { "value1", "value2" };
        }
    }
}
