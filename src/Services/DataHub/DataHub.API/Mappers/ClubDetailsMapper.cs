using AutoMapper;
using DataHub.API.Entities;
using DataHub.API.Models;

namespace DataHub.API.Mappers
{
    public class ClubDetailsMapper : Profile
    {
        public ClubDetailsMapper()
        {
            CreateMap<Club, ClubDto>()
                .ForMember(c => c.StadiumDto.Id, c => c.MapFrom(s => s.Stadium.Id))
                .ForMember(c => c.StadiumDto.FullName, c => c.MapFrom(s => s.Stadium.FullName))
                .ForMember(c => c.StadiumDto.Capacity, c => c.MapFrom(s => s.Stadium.Capacity))
                .ForMember(c => c.StadiumDto.ImageUrl, c => c.MapFrom(s => s.Stadium.ImageUrl));
            CreateMap<Stadium, StadiumDto>();
        }
    }
}
