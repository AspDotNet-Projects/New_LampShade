using System.Collections.Generic;

namespace ShopManagement.Application.Contract.Order
{
    public class Cart
    {
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount{ get; set; }
        public List<CartItem> Items;

        public Cart()
        {
            //age khali bood errore null refrence nade
            Items = new List<CartItem>();
        }

        public void Add(CartItem item)
        {
            //add kardan Item ha to list CartItem
            Items.Add(item);
            TotalAmount += item.TotalItemPrice;
            DiscountAmount += item.DiscouontAmmunt;
            PayAmount += item.ItemPayAmount;
        }
    }
}