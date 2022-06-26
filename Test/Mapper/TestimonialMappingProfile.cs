using AutoMapper;
using OngProject.Core.Models.DTOs.Testimonials;
using OngProject.Entities;

namespace Test.Mapper
{
    public class TestimonialMappingProfile : Profile
    {
        public TestimonialMappingProfile()
        {
            CreateMap<TestimonialRequest, Testimonial>();
            CreateMap<Testimonial, TestimonialResponse>();
        }
    }
}
