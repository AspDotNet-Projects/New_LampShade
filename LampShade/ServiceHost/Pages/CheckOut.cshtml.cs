using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contracts;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    public class CheckOutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly ICartCalculatorSevice _calculatorSevice;

        public CheckOutModel(ICartCalculatorSevice calculatorSevice, ICartService cartService
            ,IProductQuery productQuery)
        {
            _calculatorSevice = calculatorSevice;
            _cartService = cartService;
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            //JavaScriptSerializer() jahat tabdil cookie string to onject
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartitems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartitems)
                //calc total price in Contract 
                item.CalculateTotalItemPrice();
            Cart = _calculatorSevice.ComputeCart(cartitems);

            //set kardan cartItem to singletone
            _cartService.Set(Cart);
        }

        public IActionResult OnpostPay()
        {
            var cart = _cartService.Get();
            var result = _productQuery.CheckInventoryStatus(cart.Items);
            //age har kodam dar anbar nabod false beshe
            //Or   if (result.Any(x => x.IsInStock == false))
            if (result.Any(x => !x.IsInStock))
                    return RedirectToPage("/Cart");

            return RedirectToPage("/CheckOut");
        }
    }
}
