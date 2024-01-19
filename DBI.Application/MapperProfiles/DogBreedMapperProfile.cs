using AutoMapper;
using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Dto;

namespace DBI.Application.MapperProfiles
{
    public class DogBreedMapperProfile : Profile
    {
        public DogBreedMapperProfile()
        {
            CreateMap<DogBreedDto, DogBreed>()
                .ForMember(dest => dest.ShowName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<DogBreed, DogBreedDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ShowName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
