using DataHub.API.Contexts;
using DataHub.API.Entities;
using DataHub.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataHub.API.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly DataHubContext _context;
        private readonly ILogger<ClubRepository> _logger;

        public ClubRepository(
            DataHubContext context,
            ILogger<ClubRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Club> GetClubsDetailsByName(string query)
        {
            return _context.Club.Include(c => c.Stadium).Where(c => c.FullName.Contains(query)).OrderBy(c => c.FullName).ToList();
        }
        public void SaveClubDetails(List<Club> clubDetails)
        {
            var exisitingClubFullNames = _context.Club.Select(c => c.FullName).ToList();
            if (exisitingClubFullNames.Any())
            {
                clubDetails = clubDetails.Where(cd => !exisitingClubFullNames.Contains(cd.FullName)).ToList();
            }
            _context.Club.AddRange(clubDetails);
            _context.SaveChanges();
        }
    }
}
