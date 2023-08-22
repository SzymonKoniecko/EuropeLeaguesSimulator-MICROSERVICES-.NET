using DataHub.API.Entities;

namespace DataHub.API.Interfaces
{
    public interface IClubRepository
    {
        List<Club> GetClubsDetailsByName(string query);
        void SaveClubDetails(List<Club> clubDetails);
    }
}