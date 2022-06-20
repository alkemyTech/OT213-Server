using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using OngProject.Core.Models.DTOs.News;

namespace OngProject.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]    
    public class NewsController : ControllerBase
    {        
        private readonly INewsBusiness _newsBusiness;
        private readonly IMapper _mapper;
        public NewsController(INewsBusiness newsBusiness, IMapper mapper)
        {
            this._newsBusiness = newsBusiness;
            this._mapper = mapper;
        }

        // GET List/News
        [HttpGet]    
        [Route("List/News")]
        public  IActionResult GetAllNews() 
        {
            var news = _newsBusiness.Find(m => m.IsDeleted != true);
            return news != null ? Ok(_mapper.Map<IEnumerable<NewsGetDTO>>(news)) 
                                : NotFound("The list of news has not been found");                
        }

        // GET List/NewsById
        [HttpGet]    
        [Route("List/NewsById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var news = await _newsBusiness.GetById(id);
            return news != null ? Ok(_mapper.Map<NewsGetDTO>(news)) 
                                : NotFound($"{news.Name} news doesn't exists");            
        }

        // POST Create/News
        [HttpPost]       
        [Route("Create/News")]
        public async Task<IActionResult> Create([FromBody] NewsDTO model)
        {       
            await _newsBusiness.Insert(_mapper.Map<New>(model));                       
            return Ok(new 
            {
                Status = "Success",
                Message = $"{model.Name} news creation successfully!"
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
                    Message = "Id number doesn't match!"
                });
            } 

            var news = await _newsBusiness.GetById(id);           
            _mapper.Map(model, news);               
            var updated = await _newsBusiness.Update(news);                            
                
            return Ok(new 
            {
                Status = "Success",
                Message = $"{news.Name} news updated successfully!"
            }); 
        }

        // DELETE Delete/News/{id}
        [HttpDelete]       
        [Route("Delete/News/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var news = await _newsBusiness.GetById(id.Value);
            await _newsBusiness.SoftDelete(news);
            await _newsBusiness.Update(news);

            return Ok(new 
            {
                Status = "Success",
                Message = $"{news.Name} news deleted successfully!"
            }); 
        }
    }
}
