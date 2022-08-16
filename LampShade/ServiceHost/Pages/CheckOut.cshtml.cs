using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    public class CheckOutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        public void OnGet()
        {
            //JavaScriptSerializer() jahat tabdil cookie string to onject
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartitems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartitems)
                //calc total price in Contract 
                item.CalculateTotalItemPrice();
        }
    }
}
