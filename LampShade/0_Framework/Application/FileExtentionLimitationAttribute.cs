using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application
{
    public class FileExtentionLimitationAttribute : ValidationAttribute,IClientModelValidator
    {
        private readonly string[] _ValidExtentions;

        public FileExtentionLimitationAttribute(string[] validExtentions)
        {
            _ValidExtentions = validExtentions;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null) return true;
            var fileExtention = Path.GetExtension(file.FileName);
            //به صورت آرایه ای چک می کند
            return _ValidExtentions.Contains(fileExtention);
            //یا
            //به صورت آرایه ای چک می کند
            //if (!_ValidExtentions.Contains(fileExtention))
            //    return false;
            //return true;
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            //context.Attributes.Add("data-values", "true");
            //اگه مقدار بیشتر بود ارور مسیج بده
            //که قبلا در 
            //  create  
            //اونو مقدار دهی کردمیم
            context.Attributes.Add("data-val-fileExtentionlimite", ErrorMessage);
        }
    }
}
