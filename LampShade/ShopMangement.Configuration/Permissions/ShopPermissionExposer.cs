using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Products",new List<PermissionDto>
                       {
                        new PermissionDto(ShopPermissions.ListProducts,"ListProducts"),
                        new PermissionDto(ShopPermissions.SearchProducts,"SearchProducts"),
                        new PermissionDto(ShopPermissions.CreateProducts,"CreateProduct"),
                        new PermissionDto(ShopPermissions.EditProduct,"EditProduct"),


                       }

                },
                {
                    "ProductCategory",new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.SearchProductCategories,"SearchProductCategorties"),
                        new PermissionDto(ShopPermissions.ListProductCategories,"ListProductCategorties"),
                        new PermissionDto(ShopPermissions.CreateProductCategory,"CreateProductCategorty"),
                        new PermissionDto(ShopPermissions.EditProductCategory,"EditProductCategorty"),


                    }
                }
            };
        }
    }
}
