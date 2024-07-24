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

            CreateMap<UserDto, User>();

            CreateMap<UserDto, UserRequestDto>().ReverseMap();

            CreateMap<User, UserResponseDto>().ReverseMap();
        }
    }
}
