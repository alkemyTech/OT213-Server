using AutoMapper;
using OngProject.Core.Models.DTOs.Slides;
using OngProject.Entities;

namespace Test.Mapper
{
    public class SlideMappingProfile : Profile
    {
        public SlideMappingProfile()
        {
            CreateMap<SlideRequest, Slide>();
            CreateMap<Slide, SlideResponse>();
        }
    }
}
