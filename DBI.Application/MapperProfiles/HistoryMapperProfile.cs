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
                .ForMember(dest => dest.DogBreedId, opt => opt.MapFrom(src => src.DogBreedId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreatedDate));

            CreateMap<SearchHistoryEntity, HistoryDto>()
                .ForMember(dest => dest.DogBreedId, opt => opt.MapFrom(src => src.DogBreedId))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Date));
                //.ForMember(dest => dest.AvatarLink, opt => opt.MapFrom(src => src.DogBreed.AvatarLink))
                //.ForMember(dest => dest.DogBreedName, opt => opt.MapFrom(src => src.DogBreed.ShowName));
        }
    }
}
