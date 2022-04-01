using ShopManagement.Application.Contract.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class CreateInventory
    {   [Range(1,1000000,ErrorMessage = ValidationMesseges.IsRequired)]
        public long ProductId { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = ValidationMesseges.IsRequired)]
        public double UnitePrice { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
