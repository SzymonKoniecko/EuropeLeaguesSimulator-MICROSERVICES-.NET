using Microsoft.AspNetCore.Mvc;
using WebScrapingIntegration.API.Interfaces;
using WebScrapingIntegration.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebScrapingIntegration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubScrapingController : ControllerBase
    {
        private readonly ILogger<ClubScrapingController> _logger;
        private readonly IClubScrapingService _clubScrapingService;

        public ClubScrapingController(
            ILogger<ClubScrapingController> logger,
            IClubScrapingService clubScrapingService)
        {
            _logger = logger;
            _clubScrapingService = clubScrapingService;
        }
        /// <summary>
        /// Get Team Details via Wiki
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("/api/ClubScraping/GetClubByName/{query}", Name = "GetClubByName")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClubDetails>>> GetClubByName([FromRoute] string query)
        {
            var result = _clubScrapingService.GetClubDetailsByScraping(query);
            if (result == null)
            {
                _logger.LogWarning($"Not found any club by typing: {query}");
                return NotFound();
            }
            else
            {
                _logger.LogWarning($"Found {query.Count()} club(s) by typing: {query}");
                return Ok(result);
            }
        }

    }
}
