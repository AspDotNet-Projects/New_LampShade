using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using AccountManagement.Applicatoin.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        public EditRole command;
        public List<SelectListItem> Permissions=new List<SelectListItem>();
        private readonly IRoleApplication _roleApplication;
        /// <summary>
        /// IEnumerable<IPermissionExposer>
        /// ???? IpermissionExposer
        /// ????? ???? ????? ???? ??? ??????? ?????  ?? ????? ??? ?????
        /// ???? ??? ???? ??????? ??????
        /// </summary>
        private readonly IEnumerable<IPermissionExposer> _exposers;
        public EditModel(IRoleApplication roleApplicatio, IEnumerable<IPermissionExposer> exposers)
        {
            _roleApplication = roleApplicatio;
            _exposers = exposers;
        }

        public void OnGet(long id)
        {
            command = _roleApplication.GetDetails(id);
            ///?? ???? ?? exposer ?? ???? ????
            /// 
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

                        if (command.Mappedpermissions.Any(x => x.Code == permission.Code))
                            //age ture beshe miad to box samte resti
                            item.Selected = true;

                        Permissions.Add(item);
                    }
                }
            }

        }

        public IActionResult OnPost(EditRole command)
        {
            var result = _roleApplication.Edit(command);
            return RedirectToPage("Index");
        }
    }
}
