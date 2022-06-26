using AutoMapper;
using OngProject.Core.Models.DTOs.Comments;
using OngProject.Entities;

namespace Test.Mapper
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<CommentRequest, Comment>();
            CreateMap<Comment, CommentResponse>();
        }
    }
}
