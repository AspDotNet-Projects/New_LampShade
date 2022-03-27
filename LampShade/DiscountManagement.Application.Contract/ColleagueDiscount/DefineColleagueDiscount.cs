using System.Collections.Generic;
using ShopManagement.Application.Contract.Product;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public bool IsRemove { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
