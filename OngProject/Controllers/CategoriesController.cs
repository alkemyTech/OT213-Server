using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using System;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models;

namespace OngProject.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly ICategoriesBusiness _categoriesBusiness;
        private readonly IMapper _mapper;
        public CategoriesController(IUnitOfWork uow, ICategoriesBusiness categoriesBusiness, IMapper mapper)
        {
            this._uow = uow;
            this._categoriesBusiness = categoriesBusiness;
            this._mapper = mapper;
        }

        // GET List/Categories
        [HttpGet]    
        [Route("List/Categories")]
        public async Task<IActionResult> GetAllCategories() 
        {
            try
            {
                var categories =  _categoriesBusiness.FindCategoryAsync(m => m.softDelete != true);
                return categories != null ? Ok(_mapper.Map<IEnumerable<CategoriesDTO>>(categories)) 
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

                var member = await _categoriesBusiness.GetCategoryByIdAsync(id);
                return member != null ? Ok(_mapper.Map<CategoriesDTO>(member)) 
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
        public async Task<IActionResult> Create([FromBody] CategoriesDTO model)
        {          
            if(ModelState.IsValid)
            {
                try
                {
                    // request                    
                    await _categoriesBusiness.InsertCategoryAsync(_mapper.Map<Categories>(model));
                    await _uow.SaveAsync();                        
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
        public async Task<IActionResult> Edit(int id, [FromBody] CategoriesDTO model)
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

                    var categories = await _categoriesBusiness.GetCategoryByIdAsync(id);
                    var updated = await _categoriesBusiness.UpdateCategoryAsync(categories);

                    if(updated != null)
                    {
                        // request    
                        _mapper.Map(model,categories);               
                        await _uow.SaveAsync();           
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
                var member = await _categoriesBusiness.GetCategoryByIdAsync(id.Value);

                if(member == null)
                    return NotFound("Category not found or doesn't exist.");

                await _categoriesBusiness.SoftDelete(member, id);
                await _categoriesBusiness.UpdateCategoryAsync(member);
                await _uow.SaveAsync();

                return Ok("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
