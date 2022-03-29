using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contract.Product;

namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Range(1,100000, ErrorMessage=ValidationMesseges.IsRequired)]
        public long ProductId { get; set; }


        [Range(1, 99, ErrorMessage = ValidationMesseges.IsRequired)]
        public int DiscountRate { get; set; }
        public bool IsRemove { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
