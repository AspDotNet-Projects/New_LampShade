using AccountManagement.Applicatoin.Contracts.Account;
using AccountManagement.Applicatoin.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class CreateModel : PageModel
    {
        public SelectList Roles;
        public RegisterAccount Command;

        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        public CreateModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {

            Roles = new SelectList(_roleApplication.List(), "Id", "Name");


        }
        public IActionResult OnPost(RegisterAccount command)
        {
            var result = _accountApplication.Register(command);
            return RedirectToPage("./Index");
        }
    }
}
