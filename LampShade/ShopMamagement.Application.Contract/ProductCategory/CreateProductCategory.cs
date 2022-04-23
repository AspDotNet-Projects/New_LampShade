using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contract.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Name { get;  set; }
        public string Description { get;  set; }

        //[Required(ErrorMessage = ValidationMesseges.IsRequired)]
        [FileExtentionLimitation(new string[] {".jpeg",".jpg",".png"},ErrorMessage = ValidationMesseges.InValidFileFormat)]
        [MaxFileSize(3*1024,ErrorMessage = ValidationMesseges.MaxFileSize)]
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Keywords { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string MetaDescription { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Slug { get; set; }

    }
}
