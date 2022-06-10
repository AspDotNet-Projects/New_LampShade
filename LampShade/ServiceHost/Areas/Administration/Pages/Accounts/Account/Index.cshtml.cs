using System.Collections.Generic;
using AccountManagement.Applicatoin.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        [TempData] 
        public string Messege { get; set; }
        public List<AccountViewModel> Accounts;
        private readonly IAccountApplication _accountApplication;
        public SelectList Roles;


        public AccountSearchModel SearchModel;

        public IndexModel(IAccountApplication accountApplication)
        {
           
            _accountApplication = accountApplication;
        }


        public void OnGet(AccountSearchModel searchModel)
        {
           

            Accounts = _accountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateAccount();
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateAccount command)
        {
            var result = _accountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _accountApplication.GetDetails(id);
            
            return Partial("./Edit", product);
        }

        public JsonResult OnPostEdit(EditAccount command)
            {
            var result = _accountApplication.Edit(command);
            return new JsonResult(result);
        }




    }
}
