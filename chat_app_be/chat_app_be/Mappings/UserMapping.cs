using AutoMapper;
using chat_app_be.Dtos;
using chat_app_be.Models;

namespace chat_app_be.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

            CreateMap<UserDto, UserRequestDto>().ReverseMap();
        }
    }
}
