using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper.Interface;

namespace OngProject.Core.Helper
{
    public class AmazonHelperService : IAmazonHelperService
    {
        private readonly IAmazonS3 _amazonS3;
        public AmazonHelperService(IAmazonS3 amazonS3)
        {
            this._amazonS3 = amazonS3;
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
        //             BucketName = "cohorte-mayo-2820e45d",
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
                    BucketName = "cohorte-mayo-2820e45d",
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
        public async Task<PutObjectResponse> UploadImage(IFormFile file)
        {
            if (file == null)
                throw new Exception("The 'file' parameter is required \n");
            try
            {
                var putRequest = new PutObjectRequest()
                {
                    BucketName = "cohorte-mayo-2820e45d",
                    Key = file.FileName,
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
     
    }

}

