using AutoMapper;
using chat_app_be.Dtos;
using chat_app_be.Models;

namespace chat_app_be.Mappings
{
    public class ConversationMapping : Profile
    {
        public ConversationMapping()
        {
            CreateMap<ConversationRequestDto, ConversationResponseDto>().ReverseMap();

            CreateMap<Conversation, ConversationResponseDto>().ReverseMap();
        }
    }
}
