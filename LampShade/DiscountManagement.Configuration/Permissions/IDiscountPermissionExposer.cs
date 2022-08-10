using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace DiscountManagement.Configuration.Permissions
{
    public class IDiscountPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "CustomerDiscount",new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissioins.ListCustomersDiscounts,"ListCustomersDiscounts"),
                        new PermissionDto(DiscountPermissioins.SearchCustomersDiscounts,"SearchCustomersDiscounts"),
                        new PermissionDto(DiscountPermissioins.DefineCustomerDiscounts,"DefineCustomerDiscounts"),
                        new PermissionDto(DiscountPermissioins.EditCustomersDiscounts,"EditCustomersDiscounts"),

                    }
                },
                {
                   
                    "ColleagueDiscount",new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissioins.ListColleagueDiscounts,"ListColleagueDiscounts"),
                        new PermissionDto(DiscountPermissioins.SearchColleagueDiscounts,"SearchColleagueDiscounts"),
                        new PermissionDto(DiscountPermissioins.DefineColleagueDiscounts,"DefineColleagueDiscounts"),
                        new PermissionDto(DiscountPermissioins.EditColleagueDiscounts,"EditColleagueDiscounts"),

                    }
                }
            };
        }
    }
}
