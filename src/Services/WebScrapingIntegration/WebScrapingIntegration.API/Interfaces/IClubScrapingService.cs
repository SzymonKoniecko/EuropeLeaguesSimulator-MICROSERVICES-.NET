using WebScrapingIntegration.API.Models;

namespace WebScrapingIntegration.API.Interfaces
{
    public interface IClubScrapingService
    {
        IEnumerable<ClubDetails> GetClubDetailsByScraping(string query);
    }
}