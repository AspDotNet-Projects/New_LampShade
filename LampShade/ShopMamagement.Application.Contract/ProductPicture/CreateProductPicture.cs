using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Application.Contract.ProductPicture
{
    public class CreateProductPicture
    {

        [Range(1,100000, ErrorMessage = ValidationMesseges.IsRequired)]
        public long ProductId { get; set; }

        [MaxFileSize(1*1024*1024,ErrorMessage = ValidationMesseges.MaxFileSize)]
        public IFormFile Picture { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string PictureAlt { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string PictureTitle { get;  set; }
       
        public List<ProductViewModel> Products { get; set; }
    }
}
