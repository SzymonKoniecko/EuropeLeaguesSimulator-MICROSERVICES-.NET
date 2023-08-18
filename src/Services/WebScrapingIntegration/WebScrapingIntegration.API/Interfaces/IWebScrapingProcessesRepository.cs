using WebScrapingIntegration.API.Entities;

namespace WebScrapingIntegration.API.Interfaces
{
    public interface IWebScrapingProcessesRepository
    {
        void LogInformationAboutScraping(WebScrapingProces webScrapingProces);
        void UpdateLog(WebScrapingProces webScrapingProces);
    }
}