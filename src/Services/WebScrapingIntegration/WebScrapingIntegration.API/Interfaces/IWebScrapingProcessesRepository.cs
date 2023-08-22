using WebScrapingIntegration.API.Entities;

namespace WebScrapingIntegration.API.Interfaces
{
    public interface IWebScrapingProcessesRepository
    {
        WebScrapingProces GetScrapingProcesByGivenQuery(string query);
        void LogInformationAboutScraping(WebScrapingProces webScrapingProces);
        void UpdateLog(WebScrapingProces webScrapingProces);
    }
}