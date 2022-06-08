using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.DTOs.Categories;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentBusiness _commentBusiness;
        private readonly IMapper _mapper;

        public CommentsController(ICommentBusiness commentBusiness, IMapper mapper)
        {
            this._commentBusiness = commentBusiness;
            this._mapper = mapper;
        }


        [HttpGet]    

        public async Task<IActionResult> GetAllComment() 
        {
            try
            {
                var comments = _commentBusiness.Find(c => c.IsDeleted == false);
                return comments != null ? Ok(_mapper.Map<IEnumerable<CommentDTO>>(comments)) 
                                       : NotFound("The list of comments has not been found");                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }


        [HttpPost]       

        public async Task<IActionResult> Create([FromBody] CommentCreateDTO model)
        {          
            if(ModelState.IsValid)
            {
                try
                {
                    // request                    
                    await _commentBusiness.Insert(_mapper.Map<Comment>(model));
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }            
            return Ok(new 
            {
                Status = "Success",
                Message = "Comment creation successfully!"
            });                
        }

        //user o admin
        [HttpPut]       
        [Route("/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CommentUpdateDTO model)
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
                    var comments = await _commentBusiness.GetById(id);
                    if(comments == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Comment cannot be null."
                        });    
                    }

                    // Mapping and request
                    _mapper.Map(model, comments);               
                    var updated = await _commentBusiness.Update(comments);
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
                Message = "Comment updated successfully!"
            }); 
        }

        //user o admin
        [HttpDelete]       
        [Route("/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            // validation
            if(id == 0)
                return BadRequest("Please, set a valid ID.");

            try
            {
                var comment = await _commentBusiness.GetById(id.Value);

                if(comment == null)
                    return NotFound("Comment not found or doesn't exist.");

                await _commentBusiness.SoftDelete(comment, id);
                await _commentBusiness.Update(comment);

                return Ok("Comment deleted successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}

