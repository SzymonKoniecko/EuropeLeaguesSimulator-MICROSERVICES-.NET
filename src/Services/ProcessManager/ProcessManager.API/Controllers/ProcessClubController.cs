using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProcessManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly ILogger<ProcessController> _logger;
        private readonly IConnection _rabbitConnection;

        public ProcessController(
            ILogger<ProcessController> logger,
            IConnection connection)
        {
            _logger = logger;
            _rabbitConnection = connection;
        }
    }
}
