using AutoMapper;
using DataHub.API.Entities;
using DataHub.API.Interfaces;
using DataHub.API.Models;

namespace DataHub.API.Services
{
    public class ClubService : IClubService
    {
        private readonly ILogger<ClubService> _logger;
        private readonly IMapper _mapper;
        private readonly IClubRepository _clubRepository;

        public ClubService(
            ILogger<ClubService> logger,
            IMapper mapper,
            IClubRepository clubRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _clubRepository = clubRepository;
        }
        public List<ClubDetails> GetClubsDetailsByName(string query)
        {
            var dbResult = _clubRepository.GetClubsDetailsByName(query);
            if (dbResult == null)
            {
                return null;
            }
            List<ClubDetails> matchedClubDetails = new List<ClubDetails>();
            foreach (var item in dbResult)
            {
                matchedClubDetails.Add(_mapper.Map<ClubDetails>(_mapper.Map<ClubDto>(item)));
            }
            return matchedClubDetails;
        }
        public void SaveClubDetails(List<ClubDetails> clubDetails)
        {
            List<Club> clubs = new List<Club>();
            clubDetails.ForEach(cd =>
            {
                clubs.Add(_mapper.Map<Club>(_mapper.Map<ClubDto>(cd)));
            });
            _clubRepository.SaveClubDetails(clubs);
        }
    }
}
