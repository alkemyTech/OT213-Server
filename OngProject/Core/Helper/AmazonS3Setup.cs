using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OngProject.Core.Helper
{
    public class AmazonS3Setup
    {
        private readonly IAmazonS3 _amazonS3;
        public AmazonS3Setup(IAmazonS3 amazonS3 )
        {
            this._amazonS3 = amazonS3;
        }

        #region UPLOAD IMAGE 
        public async Task<PutObjectResponse> UploadImage(IFormFile file)
        {
            try
            {
                var putRequest = new PutObjectRequest()
                {
                    BucketName = "",
                    Key = file.FileName,
                    InputStream = file.OpenReadStream(),
                    ContentType = file.ContentType
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

        #region DOWNLOAD IMAGE 
        public async Task<FileStreamResult> DownloadImage(string imgName)
        {    
            try
            {
                var request = new GetObjectRequest()
                {
                    BucketName = "",
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

        #region DELETE IMAGE 
        [HttpDelete]       
        [Route("Delete/Image")]
        public async Task<DeleteObjectResponse> DeleteImage(string imgName)
        {
            try
            {
                var request = new DeleteObjectRequest()
                {
                    BucketName = "",
                    Key = imgName
                }; 
                var result = await _amazonS3.DeleteObjectAsync(request);

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

