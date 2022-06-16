using AccountManagement.Applicatoin.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        [TempData] 
        public string Message { get; set; }


        private readonly IAccountApplication _accountApplication;

        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin(Login commannd)
        {
            var result=_accountApplication.Login(commannd);
            if(result.IsSuccedded)
                return RedirectToPage("/Index");

            Message = result.Messege;
            return RedirectToPage("/Login");

        }

        public IActionResult OnGetLogout()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Index");
        }
    }
}
