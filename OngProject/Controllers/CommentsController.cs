using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Comments;
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
        [Authorize(Roles = "Admin")]
        [Route("/Comments")]
        public  IActionResult GetAllComments() 
        {
            var comments = _commentBusiness.Find(c => c.IsDeleted == false);
            return Ok(_mapper.Map<IEnumerable<CommentDTO>>(comments)); 
        }

        [HttpPost]
        [Route("/Comments")]
        public async Task<IActionResult> Create([FromBody] CommentCreateDTO model)
        {        
            if(model.newId == 0)
                return BadRequest("NewId cannot be null");   
            if(model.userId == 0)
                return BadRequest("UserId cannot be null");

            await _commentBusiness.Insert(_mapper.Map<Comment>(model));
            return Ok(new 
            {
                Status = "Success",
                Message = "Comment creation successfully!"
            });                
        }

        //user o admin
        [HttpPut]  
        [Authorize(Roles = "Owner, Admin")]     
        [Route("/Comments/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CommentUpdateDTO model)
        { 
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    Status = "Error",
                    Message = "Id number doesn't match!"
                });
            }  
           
            var comments = await _commentBusiness.GetById(id);
            _mapper.Map(model, comments);               
            await _commentBusiness.Update(comments);
            
            return Ok(new 
            {
                Status = "Success",
                Message = "Comment updated successfully!"
            }); 
        }

        //user o admin
        [HttpDelete]    
        [Authorize(Roles = "Owner, Admin")]     
        [Route("/Comments/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var comment = await _commentBusiness.GetById(id.Value);
            await _commentBusiness.SoftDelete(comment);
            await _commentBusiness.Update(comment);

            return Ok("Comment deleted successfully.");
        }

    }

}

