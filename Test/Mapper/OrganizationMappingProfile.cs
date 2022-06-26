using AutoMapper;
using OngProject.Core.Models.DTOs.Organizations;
using OngProject.Entities;

namespace Test.Mapper
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<OrganizationRequest, Organization>();
            CreateMap<Organization, OrganizationResponse>();
        }
    }
}
