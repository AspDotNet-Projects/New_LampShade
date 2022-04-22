using System.IO;
using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file,string path)
        {
            if (file == null) return "";
            //برای بدست آوردن مسیر
            var Directorypath = $"{_webHostEnvironment.WebRootPath}//ProductPictures//{path}";
            //اگه قبلا هنچین دسته بندی وجود نداشت و مسیری نبود اونو بساز
            if (!Directory.Exists(Directorypath))
                Directory.CreateDirectory(Directorypath);

            var filepath = $"{Directorypath}//{file.FileName}";
            //کپی کردن فایل در این مسیر
            using var output = File.Create(filepath);
            file.CopyTo(output);
            return $"{path}/{file.FileName}";
        }
    }
}
