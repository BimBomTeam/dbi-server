using AutoMapper;
using DBI.Domain.Entities.Core;
using DBI.Infrastructure.Dto;

namespace DBI.Application.MapperProfiles
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserUId))
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier));

            CreateMap<User, UserRegisterDto>()
                .ForMember(dest => dest.UserUId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier));

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier));
        }
    }
}
