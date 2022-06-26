using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        #region DOWNLOAD FILES 
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

        #region UPLOAD FILES
        public async Task<string> UploadImage(IFormFile file)
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                BucketName = _configuration["AWS:BucketName"],
                Key = file.FileName,
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
            };
            var fileTransferUtility = new TransferUtility(_amazonS3);
            await fileTransferUtility.UploadAsync(uploadRequest);
            var url = string.Format("https://{0}.s3.amazonaws.com/{1}", _configuration["AWS:BucketName"], file.FileName);
            return url;
        }
        #endregion

        #region GET FILES
        async Task<IEnumerable<string>> IAmazonHelperService.GetUrlFiles(string prefix)
        {
            var client = new AmazonS3Client();
            var request = new ListObjectsV2Request()
            {
                BucketName = _configuration["AWS:BucketName"],
                Prefix = prefix
            };
            var response = await client.ListObjectsV2Async(request);
            var presignedURL = response.S3Objects.Select(o =>
            {
                var request = new GetPreSignedUrlRequest()
                {
                    BucketName = _configuration["AWS:BucketName"],
                    Key = o.Key,
                    Expires = DateTime.Now.AddDays(1)
                };
                return client.GetPreSignedURL(request);
            });
            return presignedURL;
        }
        #endregion

    }

}