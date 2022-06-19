﻿using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace InventoryManagement.Configuration.Permissions
{
    public class InventoryPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory",new List<PermissionDto>
                    {
                        new PermissionDto(30,"SearchInventory"),
                        new PermissionDto(31,"ListInventory"),
                        new PermissionDto(32,"CreateInventory"),
                        new PermissionDto(33,"EditInventory"),


                    }
                }
            };
        }
    }
}
