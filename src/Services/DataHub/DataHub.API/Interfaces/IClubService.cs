using DataHub.API.Models;

namespace DataHub.API.Interfaces
{
    public interface IClubService
    {
        List<ClubDetails> GetClubsDetailsByName(string query);
        void SaveClubDetails(List<ClubDetails> clubDetails);
    }
}