using AccountManagement.Applicatoin.Contracts.Account;
using AccountManagement.Applicatoin.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class EditModel : PageModel
    {
        public SelectList Roles;
        public EditAccount Command;

        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        public EditModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(long id)
        {
            Command = _accountApplication.GetDetails(id);
            Roles = new SelectList(_roleApplication.List(), "Id", "Name");

        }
        public IActionResult OnPost(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
