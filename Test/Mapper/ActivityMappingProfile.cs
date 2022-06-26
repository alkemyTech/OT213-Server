using AutoMapper;
using OngProject.Core.Models.DTOs.Activities;
using OngProject.Entities;

namespace Test.Mapper
{
    public class ActivityMappingProfile : Profile
    {
        public ActivityMappingProfile()
        {
            CreateMap<ActivityRequest, Activity>();
            CreateMap<Activity, ActivityResponse>();
        }
    }
}
