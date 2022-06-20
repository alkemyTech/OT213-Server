using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Categories;
using OngProject.Entities;

namespace OngProject.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBusiness _categoryBusiness;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryBusiness categoryBusiness, IMapper mapper)
        {
            this._categoryBusiness = categoryBusiness;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("/Categories")]
        public IActionResult GetAllCategories() 
        {
            var categories =  _categoryBusiness.Find(c => c.IsDeleted == false);
            return Ok(_mapper.Map<IEnumerable<CategoryGetDTO>>(categories)); 
        }

        [HttpGet]       
        [Route("/Categories/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryBusiness.GetById(id);
            return Ok(_mapper.Map<CategoryDetailsDTO>(category)); 
        }

        [HttpPost]
        [Route("/Categories")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO model)
        {          
            await _categoryBusiness.Insert(_mapper.Map<Category>(model));
            return Ok(new 
            {
                Status = "Success",
                Message = $"{model.Name} category creation successfully!"
            });                
        }

        [HttpPut]     
        [Route("/Categories/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CategoryUpdateDTO model)
        { 
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    Status = "Error",
                    Message = "Id number doesn't match!"
                });
            }  
            
            var categories = await _categoryBusiness.GetById(id);
            _mapper.Map(model,categories);               
            await _categoryBusiness.Update(categories);
              
            return Ok(new 
            {
                Status = "Success",
                Message = $"{model.Name} category updated successfully!"
            }); 
        }

        [HttpDelete]      
        [Route("/Categories/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var category = await _categoryBusiness.GetById(id.Value);
            await _categoryBusiness.SoftDelete(category);
            await _categoryBusiness.Update(category);

            return Ok($"{category.Name} category deleted successfully.");
        }

    }

}

