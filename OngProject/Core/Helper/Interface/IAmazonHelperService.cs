using System.Threading.Tasks;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OngProject.Core.Helper.Interface
{
    public interface IAmazonHelperService
    {
        Task<PutObjectResponse> UploadImage(IFormFile file);
        Task<FileStreamResult> DownloadImage(string imgName);
        //Task<DeleteObjectResponse> DeleteImage(string imgName);
    }

}

