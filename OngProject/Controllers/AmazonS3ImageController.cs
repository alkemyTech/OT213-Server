using System.Threading.Tasks;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper;

namespace OngProject.Controllers
{
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly AmazonS3Setup _helper;
        public ImageController(IAmazonS3 amazonS3, AmazonS3Setup helper)
        {
            this._helper = helper;
        }

        // UPLOAD IMAGE
        [HttpPost]       
        [Route("Upload/Image")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {     
            await _helper.UploadImage(file);
            return Ok(new 
            {
                Status = "Success",
                Message = "Image uploaded successfully!"
            });   
        }
        
        // DOWNLOAD IMAGE
        [HttpGet]       
        [Route("Download/Image")]
        public async Task<IActionResult> DownloadImage([FromQuery] string imgName)
        {        
            await _helper.DownloadImage(imgName);
            return Ok(new 
            {
                Status = "Success",
                Message = "Image downloading..."
            });  
        }

        // DELETE IMAGE
        [HttpDelete]       
        [Route("Delete/Image")]
        public async Task<IActionResult> DeleteImage([FromQuery] string imgName)
        {
            await _helper.DeleteImage(imgName);
            return Ok(new 
            {
                Status = "Success",
                Message = "Image deleted successfully!"
            });   
        }
    }

}

