using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper.Interface;

namespace OngProject.Core.Helper
{
    public class AmazonHelperService : IAmazonHelperService
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly IConfiguration _configuration;

        public AmazonHelperService(IAmazonS3 amazonS3, IConfiguration configuration)
        {
            this._amazonS3 = amazonS3;
            this._configuration = configuration;
        }

        /*
            Delete method not implemented in this project
        */
        
        // #region DELETE IMAGE 
        // public async Task<DeleteObjectResponse> DeleteImage(string imgName)
        // {
        //     if (string.IsNullOrEmpty(imgName))
        //         throw new Exception("The 'imgName' parameter is required \n");

        //     try
        //     {
        //         var request = new DeleteObjectRequest()
        //         {
        //             BucketName = _configuration["AWS:BucketName"],
        //             Key = imgName
        //         }; 
        //         var result = await _amazonS3.DeleteObjectAsync(request);

        //         return result;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         throw new Exception(ex.Message);                
        //     }
        // }
        // #endregion

        #region DOWNLOAD IMAGE 
        public async Task<FileStreamResult> DownloadImage(string imgName)
        {
            if (string.IsNullOrEmpty(imgName))
                throw new Exception("The 'imgName' parameter is required \n");

            try
            {
                var request = new GetObjectRequest()
                {
                    BucketName = _configuration["AWS:BucketName"],
                    Key = imgName
                }; 
                using GetObjectResponse response = await _amazonS3.GetObjectAsync(request);
                using Stream responseStream = response.ResponseStream;
                var stream = new MemoryStream();
                await responseStream.CopyToAsync(stream);
                stream.Position = 0;

                return new FileStreamResult(stream, response.Headers["Content-Type"])
                {
                    FileDownloadName = imgName
                };    
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
        #endregion

        #region UPLOAD IMAGE
        public async Task<PutObjectResponse> UploadImage2(IFormFile file)
        {
            if (file == null)
                throw new Exception("The 'file' parameter is required \n");
            try
            {
                var putRequest = new PutObjectRequest()
                {
                    BucketName = _configuration["AWS:BucketName"],
                    //Key = file.FileName,
                    Key = $"OT213\\{DateTime.Now:yyyy/MM/dd/hhmmss}-{file.FileName}",
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType,
                }; 
                var result = await _amazonS3.PutObjectAsync(putRequest);

                return result;                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UPLOAD IMAGE
        public async Task<PutObjectResponse> UploadImage(IFormFile file)//, string url)//, GetObjectResponse get)
        {
            if (file == null)
                throw new Exception("The 'file' parameter is required \n");
            try
            {

                //Byte[] bArray = File.ReadAllBytes(url);
                //String base64String = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(url));
                var url = string.Format("https://{0}.s3.amazonaws.com/{1}", _configuration["AWS:BucketName"], file.FileName);
                var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(url));
                var base64EncodedBytes = Convert.FromBase64String(base64);
                var URLImage = Encoding.UTF8.GetString(base64EncodedBytes);

                //byte[] bytes = Convert.FromBase64String(base64);
                //var URLImage = File.WriteAllBytes(bytes);

                // MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(URLImage));
                // var fileString = file.CopyToAsync(mStream);

                var putRequest = new PutObjectRequest()
                {
                    BucketName =  _configuration["AWS:BucketName"],
                    Key = $"OT213\\{DateTime.Now:yyyy/MM/dd/hhmmss}-{file.FileName}", //string.Format(),//string.Format("bucketName/{0}", "foo.jpg")
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType,
                };       

                //var a = putRequest.FilePath;        
                //"https://cohorte-mayo-2820e45d.s3.amazonaws.com/OIP.jpg?AWSAccessKeyId=AKIAS2JWQJCDPYY77JXE&Expires=1656077669&Signature=P3tlX67F4IKAyKedBv240gyt8x8%3D"
                //var Key = $"OT213\\{DateTime.Now:yyyy/MM/dd/hhmmss}-{file.FileName}";  
                
                var result = await _amazonS3.PutObjectAsync(putRequest);

                return result;                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // public void Encryption(IFormFile file)
        // {
        //     var url = string.Format("https://{0}.s3.amazonaws.com/{1}", _configuration["AWS:BucketName"], file.FileName);
        //     var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(url));
        // }

        // public void Decode(IFormFile file)
        // {
        //     var base64EncodedBytes = Convert.FromBase64String(base64);
        //     var URLImage = Encoding.UTF8.GetString(base64EncodedBytes);
        // }

        #endregion

        #region Encryption & Decode
        public string DecodeFile(IFormFile file)
        {
            var url = string.Format("https://{0}.s3.amazonaws.com/{1}", _configuration["AWS:BucketName"], file.FileName);
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(url));
            var base64EncodedBytes = Convert.FromBase64String(base64);
            var URLImage = Encoding.UTF8.GetString(base64EncodedBytes);

            return URLImage;
        }
        #endregion

    }

}

