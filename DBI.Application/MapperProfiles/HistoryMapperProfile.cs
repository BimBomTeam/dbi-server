using AutoMapper;
using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Dto;

namespace DBI.Application.MapperProfiles
{
    public class HistoryMapperProfile : Profile
    {
        public HistoryMapperProfile()
        {
            CreateMap<HistoryDto, SearchHistoryEntity>()
                .ForMember(dest => dest.DogBreedId, opt => opt.MapFrom(src => src.DogBreedId));

            CreateMap<SearchHistoryEntity, HistoryDto>()
                .ForMember(dest => dest.DogBreedId, opt => opt.MapFrom(src => src.DogBreedId));
        }
    }
}
