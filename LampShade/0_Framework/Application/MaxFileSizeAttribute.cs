using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application
{
    public class MaxFileSizeAttribute : ValidationAttribute,IClientModelValidator
    {
        //سایز فایل رو داشته باشیم و کارهای مختلفی  رو روی آن انجام بدیم
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxfilesize)
        {
            _maxFileSize = maxfilesize;
        }

        /// <summary>
        /// متد ایز ولید قیلا وجود دارد یعنی از متد های سیستمی است به همی خاطر آنرا
        /// override  کردیم
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            //بیدیل ولیو به آبجکت 
            var file = value as IFormFile;
            if (file != null) return true;
            //به این معنی اگر کوچکتر با مساوی بود ترو برگردون در غیر اینصورت فالس
            return file.Length <= _maxFileSize;
        }

        /// <summary>
        /// این اد ولیدیشن کمک میکنه ولیدیشن سمت کلاینت داشته باشیم
        /// </summary>
        /// <param name="context"></param>
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val","true");
            //اگه مقدار بیشتر بود ارور مسیج بده
            //که قبلا در 
            //  create  
            //اونو مقدار دهی کردمیم
            context.Attributes.Add("data-val-maxFileSize",ErrorMessage);
        }
    }
}
