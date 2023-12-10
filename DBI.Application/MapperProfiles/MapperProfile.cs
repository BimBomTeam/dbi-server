using AutoMapper;
using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Dto;

namespace DBI.Application.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DogBreedDto, DogBreed>()
                .ForMember(dest => dest.ShowName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.Description));
            CreateMap<DogBreed, DogBreedDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ShowName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ShortDescription));
        }
    }
}
