using System.Threading.Tasks;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OngProject.Controllers
{
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IAmazonS3 _amazonS3;
        public ImageController(IAmazonS3 amazonS3 )
        {
            this._amazonS3 = amazonS3;
        }

        // GET Image
        [HttpGet]       
        [Route("Show/Image")]
        public async Task<IActionResult> GetImage()
        {          
            return Ok(new 
            {
                Status = "Success",
                Message = "Image uploaded successfully!"
            });   
        }

        // POST Upload Image
        [HttpPost]       
        [Route("Upload/Image")]
        public async Task<IActionResult> UploadImage(IFormFile iformFile)
        {          
            return Ok(new 
            {
                Status = "Success",
                Message = "Image uploaded successfully!"
            });   
        }
    }

}

