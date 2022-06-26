using Microsoft.AspNetCore.Http;
using System.IO;

namespace Test.Helper
{
    public static class ImageHelper
    {
        public static IFormFile CreateImage(string filename)
        {
            var imageCurrentPath = Directory.GetCurrentDirectory();
            var index = imageCurrentPath.IndexOf("Test\\");
            var finalPath = imageCurrentPath.Substring(0, index + 4) + "\\" + filename;
            var imageFile = File.OpenRead(finalPath);

            IFormFile newFile = new FormFile(imageFile, 0, imageFile.Length, "logoAlkemy", filename)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
            return newFile;
        }
    }
}