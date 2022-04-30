using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contract.Slide
{
    public class CreateSlide
    {
        [MaxFileSize(3*1024*1024,ErrorMessage = ValidationMesseges.MaxFileSize)]
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Heading { get; set; }
        
        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Text { get; set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Link { get; set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string BtnText { get; set; }
        public string Btncolor { get; set; }
    }
}
