using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Name { get;  set; }

        public IFormFile Picture { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Description { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public int ShowOrder { get;  set; }
        
        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Slug { get;  set; }
        
        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Keywords { get;  set; }
        
        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string MetaDescription { get;  set; }
        public string CanonicalAddress { get;  set; }
    }
}
