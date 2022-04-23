using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Application.Contract.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage =ValidationMesseges.IsRequired)]
        public string Name { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Code { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string ShortDescription { get;  set; }
        public string Description { get;  set; }

        [FileExtentionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMesseges.InValidFileFormat)]
        [MaxFileSize(3 * 1024, ErrorMessage = ValidationMesseges.MaxFileSize)]
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }

        //for int or long data
        [Range(1,1000000,ErrorMessage = ValidationMesseges.IsRequired)]
        public long CategoryId { get;  set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }


        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string MetaDescription { get; set; }
        //جهت مفداری دهی لیست بازشو در فرم ایجاد محصول
        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}
