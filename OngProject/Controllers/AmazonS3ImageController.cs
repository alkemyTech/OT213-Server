using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper.Interface;

namespace OngProject.Controllers
{
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IAmazonHelperService _aws;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ImageController(IAmazonHelperService aws, IConfiguration config, IWebHostEnvironment hostingEnvironment)
        {
            this._aws = aws;
            this._config = config;
            this._hostingEnvironment = hostingEnvironment;
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

        [HttpPost]        
        [Route("test")]
        public async Task<IActionResult> Upload(IFormFile file) 
        {
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            // foreach (IFormFile file in files) {
            //     if (file.Length > 0) {
                    string filePath = Path.Combine(uploads, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create)) 
                    {
                        await file.CopyToAsync(fileStream);
                    }
                //}
            //}
            return Ok();
        }

        [HttpGet]       
        [Route("get/URLfiles")]
        public async Task<IActionResult> GetURLFiles(string prefix)
        {       
            var client = new AmazonS3Client();
            var request = new ListObjectsV2Request()
            {
                BucketName = _config["AWS:BucketName"],
                Prefix = prefix
            };
            var response = await client.ListObjectsV2Async(request);
            var presignedURL = response.S3Objects.Select(o =>
            {
                var request = new GetPreSignedUrlRequest()
                {
                    BucketName = _config["AWS:BucketName"],
                    Key = o.Key,
                    Expires = DateTime.Now.AddDays(1)
                };
                return client.GetPreSignedURL(request);
            });
            return Ok(presignedURL);     
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

        /*
            Delete method not implemented in this project
        */
        // // DELETE IMAGE
        // [HttpDelete]       
        // [Route("Delete/Image")]
        // public async Task<IActionResult> DeleteImage([FromQuery] string imgName)
        // {
        //     await _aws.DeleteImage(imgName);
        //     return Ok(new 
        //     {
        //         Status = "Success",
        //         Message = "Image deleted successfully!"
        //     });   
        // }
    }

}

