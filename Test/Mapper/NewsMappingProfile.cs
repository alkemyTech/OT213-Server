using AutoMapper;
using OngProject.Core.Models.DTOs.News;
using OngProject.Entities;

namespace Test.Mapper
{
    public class NewsMappingProfile : Profile
    {
        public NewsMappingProfile()
        {
            CreateMap<NewsRequest, News>();
            CreateMap<News, NewsResponse>();
        }
    }
}
