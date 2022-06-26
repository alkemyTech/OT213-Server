using AutoMapper;
using OngProject.Core.Models.DTOs.Categories;
using OngProject.Entities;

namespace Test.Mapper
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>();
        }
    }
}
