using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Helper.Interface
{
    public interface IAmazonHelperService
    {
        Task<string> UploadImage(IFormFile file);
        Task<FileStreamResult> DownloadImage(string imgName);
        Task<IEnumerable<string>> GetUrlFiles(string prefix);
    }

}

