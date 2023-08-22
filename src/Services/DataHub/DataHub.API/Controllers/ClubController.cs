using DataHub.API.Interfaces;
using DataHub.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly ILogger<ClubController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IClubService _clubService;

        public ClubController(
            ILogger<ClubController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IClubService clubService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _clubService = clubService;
        }
        [HttpGet("{query:alpha}")]
        public async Task<ActionResult<ClubDetails>> GetClubsByName([FromRoute] string query)
        {
            var matchedClubs = _clubService.GetClubsDetailsByName(query);
            if (matchedClubs.Count > 0)
            {
                return Ok(matchedClubs);
            }
            else
            {
                _logger.LogWarning($"Needs to scrap data for {query}");
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_configuration.GetSection("WebScrapingIntegrationAPIGwURLDocker").Value); // Ustawienie bazy dla Ocelota

                try
                {
                    // Tworzenie URI dla endpointu Ocelota
                    var requestUri = $"api/ClubScraping/{query}"; // Zmienić "your-query-here" na rzeczywisty query

                    //httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0"); // Przykład nagłówka User-Agent
                    //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer YourAccessToken"); // Przykład nagłówka autoryzacji
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/plain"));
                    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); // Nagłówek Accept
                    //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json"); // Nagłówek Content-Type


                    // Wykonanie żądania GET
                    var response = await httpClient.GetAsync(requestUri);

                    if (response.IsSuccessStatusCode)
                    {
                        // Przetwarzanie udanej odpowiedzi
                        var content = await response.Content.ReadAsStringAsync();
                        IEnumerable<ClubDetails> result = JsonConvert.DeserializeObject<IEnumerable<ClubDetails>>(content);
                        if (result is not null)
                        {
                            _clubService.SaveClubDetails(result.ToList());
                        }
                        httpClient.Dispose();
                        return Ok(_clubService.GetClubsDetailsByName(query));
                    }
                    else
                    {
                        httpClient.Dispose();
                        return StatusCode((int)response.StatusCode, response.Content);
                    }
                }
                catch (Exception ex)
                {
                    httpClient.Dispose();
                    return StatusCode(500, "An error occurred: " + ex.Message);
                }
            }

        }
    }
}
