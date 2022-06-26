using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper.Interface;

namespace OngProject.Controllers
{
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IAmazonHelperService _aws;

        public ImageController(IAmazonHelperService aws)
        {
            this._aws = aws;
        }

        // UPLOAD IMAGE
        [HttpPost]       
        [Route("Upload/Image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {     
            await _aws.UploadImage(file);
            return Ok(new 
            {
                Status = "Success",
                Message = "Image uploaded successfully!"
            }); 
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("get/URLfiles")]
        public async Task<IActionResult> GetURLFiles(string prefix)
        {
            return Ok(await _aws.GetUrlFiles(prefix));
        }

        // DOWNLOAD IMAGE
        [HttpGet]
        [Route("Download/Image")]
        public async Task<IActionResult> DownloadImage([FromQuery] string imgName)
        {
            await _aws.DownloadImage(imgName);
            return Ok(new
            {
                Status = "Success",
                Message = "Image downloading..."
            });
        }
    }

}

