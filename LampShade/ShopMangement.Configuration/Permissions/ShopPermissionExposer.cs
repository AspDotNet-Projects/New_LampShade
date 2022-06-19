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
                        new PermissionDto(10,"ListProducts"),
                        new PermissionDto(11,"SearchProducts"),
                        new PermissionDto(12,"CreateProduct"),
                        new PermissionDto(13,"EditProducts"),


                       }

                },
                {
                    "ProductCategory",new List<PermissionDto>
                    {
                        new PermissionDto(20,"SearchProductCategorties"),
                        new PermissionDto(21,"ListProductCategorties"),
                        new PermissionDto(22,"CreateProductCategorty"),
                        new PermissionDto(23,"EditProductCategorty"),


                    }
                }
            };
        }
    }
}
