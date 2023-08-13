using Logger.API.Context;
using Logger.API.Interfaces;
using Logger.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Logger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly IAppLogRepository _appLogRepository;
        private readonly LoggerContext _loggerContext;

        public LogController(IAppLogRepository appLogRepository, LoggerContext loggerContext)
        {
            _appLogRepository = appLogRepository;
            _loggerContext = loggerContext;
        }
        // GET: api/<LogController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LogController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LogController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LogController>/5
        [HttpPut]
        public async Task<ActionResult> Put()
        {
            var obj = new AppLogDto()
            {
                Id = Guid.NewGuid(),
                Application = "Logger",
                LoggedTime = DateTime.Now,
                Level = "info",
                Message = "test",
                Logger = "test",
                Callsite = "test",
                Exception = "test"

            };
            _appLogRepository.SaveLog(obj);
            return Ok(_loggerContext.Database.CanConnect());
        }
        // DELETE api/<LogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
