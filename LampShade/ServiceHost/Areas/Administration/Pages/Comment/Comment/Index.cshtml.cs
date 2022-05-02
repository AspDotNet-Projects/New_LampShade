using System.Collections.Generic;
using CommentManagement.Application.Contract.ProductComment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Slide;

namespace ServiceHost.Areas.Administration.Pages.Comment.Comment
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public CommentSearchModel SearchModel;
        public List<CommentViewModel> productComments;

        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication productcommentapplication)
        {
            _commentApplication = productcommentapplication;
        }


        public void OnGet(CommentSearchModel searchModel)
        {
            productComments = _commentApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);

        }


        public IActionResult OnGetCancel(long id)
        {
           var result = _commentApplication.Cancel(id);
           if (result.IsSuccedded)
               return RedirectToPage("./Index");
           Message = result.Messege;
           return RedirectToPage("./Index");


        }

        public IActionResult OnGetConfirm(long id)
        {
            var result = _commentApplication.Confirm(id);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            Message = result.Messege;
            return RedirectToPage("./Index");
        }
    }
}
