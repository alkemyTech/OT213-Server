using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using System;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs;

namespace OngProject.Controllers
{
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly INewsBusiness _newsBusiness;
        private readonly IMapper _mapper;
        public NewsController(IUnitOfWork uow, INewsBusiness newsBusiness, IMapper mapper)
        {
            this._uow = uow;
            this._newsBusiness = newsBusiness;
            this._mapper = mapper;
        }

        // GET List/News
        [HttpGet]    
        [Route("List/News")]
        public async Task<IActionResult> GetAllNews() 
        {
            try
            {
                var news = _newsBusiness.Find(m => m.softDelete != true);
                return news != null ? Ok(_mapper.Map<IEnumerable<NewsDTO>>(news)) 
                                       : NotFound("The list of news has not been found");                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        // GET List/NewsById
        [HttpGet]        
        [Route("List/NewsById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if(id == 0)
                    return NotFound("Please, set an ID.");

                var news = await _newsBusiness.GetById(id);
                return news != null ? Ok(_mapper.Map<NewsDTO>(news)) 
                                      : NotFound("News doesn't exists");            
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST Create/News
        [HttpPost]       
        [Route("Create/News")]
        public async Task<IActionResult> Create([FromBody] NewsDTO model)
        {          
            if(ModelState.IsValid)
            {
                try
                {
                    // validations                    
                    if(string.IsNullOrEmpty(model.Name))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Name is required"
                        });
                    }
                    if(string.IsNullOrEmpty(model.Content))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Content is required"
                        });
                    }

                    // request                    
                    await _newsBusiness.Insert(_mapper.Map<New>(model));
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
                Message = "News creation successfully!"
            });                
        }

        // PUT Update/News/{id}
        [HttpPut]       
        [Route("Update/News/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] NewsDTO model)
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
                    // validations                    
                    if(string.IsNullOrEmpty(model.Name))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Name is required"
                        });
                    }
                    if(string.IsNullOrEmpty(model.Content))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Content is required"
                        });
                    }

                    var news = await _newsBusiness.GetById(id);
                    var updated = await _newsBusiness.Update(news);

                    if(updated != null)
                    {
                        // request    
                        _mapper.Map(model, news);               
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
                Message = "News updated successfully!"
            }); 
        }

        // DELETE Delete/News/{id}
        [HttpDelete]       
        [Route("Delete/News/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            // validation
            if(id == 0)
                return NotFound("Please, set a valid ID.");

            try
            {
                var news = await _newsBusiness.GetById(id.Value);

                if(news == null)
                    return NotFound("News not found or doesn't exist.");

                await _newsBusiness.SoftDelete(news, id);
                await _newsBusiness.Update(news);
                await _uow.SaveAsync();

                return Ok("News deleted successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
