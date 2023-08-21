using AutoMapper;
using DataHub.API.Contexts;
using DataHub.API.Entities;
using DataHub.API.Interfaces;
using DataHub.API.Models;

namespace DataHub.API.Services
{
    public class ClubService : IClubService
    {
        private readonly DataHubContext _context;
        private readonly ILogger<ClubService> _logger;
        private readonly IMapper _mapper;

        public ClubService(
            DataHubContext context,
            ILogger<ClubService> logger,
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public List<ClubDetails> GetClubsDetailsByName(string query)
        {
            //var clubDetails = _context.Club.

            return null;
        }
        public void SaveClubDetails(List<ClubDetails> clubDetails)
        {
            List<Club> clubDtos = new List<Club>();
            clubDetails.ForEach(cd =>
            {
                clubDtos.Add(_mapper.Map<Club>(_mapper.Map<ClubDto>(cd)));
            });
            _context.AddRange(clubDtos);
            _context.SaveChanges();
        }
    }
}
