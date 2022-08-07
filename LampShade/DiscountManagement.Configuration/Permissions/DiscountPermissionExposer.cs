using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace DiscountManagement.Configuration.Permissions
{
    public class DiscountPermissionExposer : IPermissionExposer
    {

        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "CustomerDiscount", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.listCustomerDiscount, "listCustomerDiscount"),
                        new PermissionDto(DiscountPermissions.DefineCustomerDiscount, "DefineCustomerDiscount"),
                        new PermissionDto(DiscountPermissions.EditCustomerDiscount, "EditCustomerDiscount"),
                        new PermissionDto(DiscountPermissions.SearchCustomerDiscount, "SearchCustomerDiscount"),
                        new PermissionDto(DiscountPermissions.ActiveCustomerDiscount, "ActiveCustomerDiscount"),

                    }
                },
                {
                    "ColleagueDiscount", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.listColleagueDiscount, "listColleagueDiscount"),
                        new PermissionDto(DiscountPermissions.DefineColleagueDiscount, "DefineColleagueDiscount"),
                        new PermissionDto(DiscountPermissions.EditColleagueDiscount, "EditColleagueDiscount"),
                        new PermissionDto(DiscountPermissions.SearchColleagueDiscount, "SearchColleagueDiscount"),
                        new PermissionDto(DiscountPermissions.ActiveColleagueDiscount, "ActiveColleagueDiscount"),

                    }
                }
            };
        }
    }
}
