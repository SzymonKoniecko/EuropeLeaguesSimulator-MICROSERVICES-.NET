using AutoMapper;
using DataHub.API.Entities;
using DataHub.API.Models;
using System.Globalization;

namespace DataHub.API.Mappers
{
    public class ClubMapper : Profile
    {
        public ClubMapper()
        {
            CreateMap<Stadium, StadiumDto>();
            CreateMap<StadiumDto, Stadium>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<Club, ClubDto>()
            .ForMember(dest => dest.StadiumDto, opt => opt.MapFrom(src => src.Stadium))
            .ReverseMap()
            .ForMember(dest => dest.StadiumId, opt => opt.MapFrom(src => src.StadiumDto.Id))
            .AfterMap((src, dest, context) =>
            {
                dest.Stadium = context.Mapper.Map<Stadium>(src.StadiumDto);
                dest.Stadium.Id = dest.StadiumId;
            });
            CreateMap<ClubDto, Club>()
                .ForMember(dest => dest.StadiumId, opt => opt.MapFrom(src => src.StadiumDto.Id))
                .AfterMap((src, dest, context) =>
                {
                    dest.Stadium = context.Mapper.Map<Stadium>(src.StadiumDto);
                    dest.Stadium.Id = dest.StadiumId;
                });



            CreateMap<ClubDetails, ClubDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.LogoUrl))
                .ForMember(dest => dest.StadiumDto, opt => opt.MapFrom(src => src));

            CreateMap<ClubDetails, StadiumDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.StadiumFullName))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => ConvertCapacity(src.StadiumCapacity)))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.StadiumImageUrl));
        }
        private int ConvertCapacity(string capacityString)
        {
            capacityString = capacityString.Replace(",", ""); // Usunięcie przecinka
            int capacity;
            int.TryParse(capacityString, NumberStyles.Any, CultureInfo.InvariantCulture, out capacity);
            return capacity;
        }
    }
}
