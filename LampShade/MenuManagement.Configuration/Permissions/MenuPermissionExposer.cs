using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace MenuManagement.Configuration.Permissions
{
    public class MenuPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Menu",new List<PermissionDto>()
                    {
                        new PermissionDto(MenuPermissions.Comments,"نظرات"),
                        new PermissionDto(MenuPermissions.Inventory,"انبارداری"),
                        new PermissionDto(MenuPermissions.Shop,"فروشگاه"),
                        new PermissionDto(MenuPermissions.Discount,"تخفیفات"),
                        new PermissionDto(MenuPermissions.Users,"کاربران"),
                        new PermissionDto(MenuPermissions.Blogin,"مقالات"),
                    }
                }
            };
        }


    }
}
