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
    [Route("/categories")]
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

        // GET 
        [HttpGet]    
        public async Task<IActionResult> GetAllCategories() 
        {
            try
            {
                var categories =  _categoriesBusiness.FindCategoryAsync(m => m.softDelete != true);
                return categories != null ? Ok(_mapper.Map<IEnumerable<Core.Models.CategoriesGetDTO>>(categories)) 
                                       : NotFound("The list of categories has not been found");                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        [HttpGet]        
        [Route("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if(id == 0)
                    return BadRequest("Please, set an ID.");

                var member = await _categoriesBusiness.GetCategoryByIdAsync(id);
                return member != null ? Ok(_mapper.Map<CategoriesPostDTO>(member)) 
                                      : NotFound("Member doesn't exists");            
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]       
        public async Task<IActionResult> Create([FromBody] CategoriesPostDTO model)
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


        [HttpPut]       
        [Route("/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CategoriesUpdateDTO model)
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

        [HttpDelete]       
        [Route("/{id}")]
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
