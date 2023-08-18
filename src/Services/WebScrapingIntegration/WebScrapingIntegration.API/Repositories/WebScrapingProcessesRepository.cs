using WebScrapingIntegration.API.Contexts;
using WebScrapingIntegration.API.Entities;
using WebScrapingIntegration.API.Interfaces;

namespace WebScrapingIntegration.API.Repositories
{
    public class WebScrapingProcessesRepository : IWebScrapingProcessesRepository
    {
        private readonly ILogger<WebScrapingProcessesRepository> _logger;
        private readonly WebScrapingProcessesContext _context;

        public WebScrapingProcessesRepository(
            ILogger<WebScrapingProcessesRepository> logger,
            WebScrapingProcessesContext context
            )
        {
            _logger = logger;
            _context = context;
        }
        public void LogInformationAboutScraping(WebScrapingProces webScrapingProces)
        {
            try
            {
                _context.Add(webScrapingProces);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Information not saved about web scraping process for query {webScrapingProces.GivenQuery} - {webScrapingProces.Id}");
                throw;
            }
        }
        public void UpdateLog(WebScrapingProces webScrapingProces)
        {
            try
            {
                _context.Update(webScrapingProces);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Information not updated about web scraping process for query {webScrapingProces.GivenQuery} - {webScrapingProces.Id}");
                throw;
            }
        }
    }
}
