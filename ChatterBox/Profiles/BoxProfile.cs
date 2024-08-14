using AutoMapper;
using ChatterBox.DTO.Requests.Box;
using ChatterBox.DTO.Responses.Box;
using ChatterBox.Entities;

namespace ChatterBox.Profiles
{
    public class BoxProfile: Profile
    {
        public BoxProfile() {
            // Mapping request DTO to entity 
            CreateMap<PostBoxDto, Box>();
            CreateMap<PutBoxDto, Box>(); 

            // Mapping entity to response DTO
            CreateMap<Box, GetBoxDto>();
        }
    }
}
