using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Categories;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBusiness _categoryBusiness;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryBusiness categoryBusiness, IMapper mapper)
        {
            this._categoryBusiness = categoryBusiness;
            this._mapper = mapper;
        }

         // GET List/Categories
        [HttpGet]    
        [Route("List/Categories")]
        public async Task<IActionResult> GetAllCategories() 
        {
            try
            {
                var categories =  _categoryBusiness.Find(c => c.IsDeleted == false);
                return categories != null ? Ok(_mapper.Map<IEnumerable<CategoryGetDTO>>(categories)) 
                                       : NotFound("The list of categories has not been found");                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        // GET List/CategoriesById
        [HttpGet]        
        [Route("List/CategoriesById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if(id == 0)
                    return BadRequest("Please, set an ID.");

                var member = await _categoryBusiness.GetById(id);
                return member != null ? Ok(_mapper.Map<CategoryGetDTO>(member)) 
                                      : NotFound("Member doesn't exists");            
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST Create/Category
        [HttpPost]       
        [Route("Create/Category")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO model)
        {          
            if(ModelState.IsValid)
            {
                try
                {
                    // request                    
                    await _categoryBusiness.Insert(_mapper.Map<Category>(model));
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }            
            return Ok(new 
            {
                Status = "Success",
                Message = "Category creation successfully!"
            });                
        }

        // PUT Update/Category/{id}
        [HttpPut]       
        [Route("Update/Category/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CategoryUpdateDTO model)
        { 
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "Id number not found!"
                });
            }  

            if(ModelState.IsValid)
            {
                try
                {
                    var categories = await _categoryBusiness.GetById(id);
                    if(categories == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Category cannot be null."
                        });    
                    }

                    // Mapping and request
                    _mapper.Map(model,categories);               
                    var updated = await _categoryBusiness.Update(categories);
                    if(updated != null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Error updating data"
                        });       
                    }
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return Ok(new 
            {
                Status = "Success",
                Message = "Category updated successfully!"
            }); 
        }

        // DELETE Delete/Category/{id}
        [HttpDelete]       
        [Route("Delete/Category/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            // validation
            if(id == 0)
                return BadRequest("Please, set a valid ID.");

            try
            {
                var category = await _categoryBusiness.GetById(id.Value);

                if(category == null)
                    return NotFound("Category not found or doesn't exist.");

                await _categoryBusiness.SoftDelete(category, id);
                await _categoryBusiness.Update(category);

                return Ok("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}

