using _0_Framework.Infrastructure;
using AccountManagement.Applicatoin.Contracts.Account;
using AccountManagement.Applicatoin.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class EditModel : PageModel
    {
        public SelectList Roles;
        public EditAccount Command;

        public List<SelectListItem> Permissions = new List<SelectListItem>();

        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;
        public EditModel(IAccountApplication accountApplication, IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
            _exposers = exposers;
        }

        public void OnGet(long id)
        {
            Command = _accountApplication.GetDetails(id);
            Roles = new SelectList(_roleApplication.List(), "Id", "Name");

            foreach (var exposer in _exposers)
            {
                //???? ??? ?? ????? 
                var exposedPermissions = exposer.Expose();
                //???? ?? ??? ?? ????? ???? ? ??????? ???????? ?? ???? ???
                foreach (var (key, value) in exposedPermissions)
                {

                    var group = new SelectListGroup { Name = key };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };

                        if (Command.Mappedpermissions.Any(x => x.Code == permission.Code))
                            //age ture beshe miad to box samte resti
                            item.Selected = true;

                        Permissions.Add(item);
                    }
                }
            }

        }
        public IActionResult OnPost(EditAccount command)
        {
            var result = _accountApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
