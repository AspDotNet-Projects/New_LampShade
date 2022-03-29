using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.Contract.Product;

namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    /// <summary>
    /// تعریف تخفیف برای مشتری
    /// </summary>
    public class DefineCustomerDiscount
    {
        [Range(1, 100000, ErrorMessage = ValidationMesseges.IsRequired)]
        public long ProductId { get;  set; }

        [Range(1, 99, ErrorMessage = ValidationMesseges.IsRequired)]
        public int DiscountRate { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string StartDate { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string EndDate { get;  set; }
        public string Reason { get;  set; }
        public List<ProductViewModel> Products { get; set; }


    }
}
