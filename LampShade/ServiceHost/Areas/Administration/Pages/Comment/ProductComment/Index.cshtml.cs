using System.Collections.Generic;
using CommentManagement.Application.Contract.ProductComment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Slide;

namespace ServiceHost.Areas.Administration.Pages.Comment.ProductComment
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public ProductCommentSearchModel SearchModel;
        public List<ProductCommentViewModel> productComments;

        private readonly IProductCommentApplication _productCommentApplication;

        public IndexModel(IProductCommentApplication productcommentapplication)
        {
            _productCommentApplication = productcommentapplication;
        }


        public void OnGet(ProductCommentSearchModel searchModel)
        {
            productComments = _productCommentApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);

        }


        public IActionResult OnGetCancel(long id)
        {
           var result = _productCommentApplication.Cancel(id);
           if (result.IsSuccedded)
               return RedirectToPage("./Index");
           Message = result.Messege;
           return RedirectToPage("./Index");


        }

        public IActionResult OnGetConfirm(long id)
        {
            var result = _productCommentApplication.Confirm(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            Message = result.Messege;
            return RedirectToPage("./Index");
        }
    }
}
