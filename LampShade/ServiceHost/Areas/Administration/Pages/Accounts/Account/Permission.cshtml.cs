using _0_Framework.Infrastructure;
using AccountManagement.Applicatoin.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using AccountManagement.Applicatoin.Contracts.Role;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class PermissionModel : PageModel
    {
        public EditAccount Command;
        public EditRole Role;
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;
        public List<SelectListItem> Permissions = new List<SelectListItem>();

        public PermissionModel(IAccountApplication accountApplication, IEnumerable<IPermissionExposer> exposers, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _exposers = exposers;
            _roleApplication = roleApplication;
        }

        public void OnGet(long id)
        {
            Command = _accountApplication.GetDetails(id);
            Role = _roleApplication.GetDetails(Command.RoleId);
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
            var result = _accountApplication.EditPermission(command);
            return RedirectToPage("./Index");
        }
    }
}
