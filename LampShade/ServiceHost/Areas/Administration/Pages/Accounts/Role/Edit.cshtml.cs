using AccountManagement.Application;
using AccountManagement.Applicatoin.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        public EditRole command;
        private readonly IRoleApplication _roleApplication;

        public EditModel(IRoleApplication roleApplicatio)
        {
            _roleApplication = roleApplicatio;
            
        }

        public void OnGet(long id)
        {
            command = _roleApplication.GetDetails(id);
          
        }

        public IActionResult OnPost(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            return RedirectToPage("Index");
        }
    }
}
