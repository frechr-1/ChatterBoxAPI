using AutoMapper; 
using ChatterBox.DTO.Requests.ChatBox; 
using ChatterBox.DTO.Responses.ChatBox;
using ChatterBox.Entities;

namespace ChatterBox.Profiles
{
    public class ChatBoxProfile: Profile
    {
        public ChatBoxProfile() {
            // Mapping request DTO to entity 
            CreateMap<PostChatBoxDto, ChatBox>();
            CreateMap<PutChatBoxDto, ChatBox>(); 

            // Mapping entity to response DTO
            CreateMap<ChatBox, GetChatBoxDto>();

        }
    }
}
