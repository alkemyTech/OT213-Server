using Microsoft.AspNetCore.Http;
using System;

namespace OngProject.Core.Helper.Interface
{
    public static class AWSMockWithOutCredentials
    {
        public static string UploadImage(IFormFile file)
        {
            return string.Format("https://{0}.s3.amazonaws.com/{1}", Guid.NewGuid(), file.FileName);
        }
    }
}
