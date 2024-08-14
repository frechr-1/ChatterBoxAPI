using AutoMapper;
using ChatterBox.DTO.Requests.BoxComment; 
using ChatterBox.DTO.Responses.BoxComment; 
using ChatterBox.Entities;

namespace ChatterBox.Profiles
{
    public class BoxCommentProfile : Profile
    {
        public BoxCommentProfile()
        {
            // Mapping request DTO to entity 
            CreateMap<PostBoxCommentDto, BoxComment>();
            CreateMap<PutBoxCommentDto, BoxComment>();

            // Mapping entity to response DTO
            CreateMap<BoxComment, GetBoxCommentDto>();

        }
    }
}
