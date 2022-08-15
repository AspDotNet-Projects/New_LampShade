using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";

        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            //baraye jelo giri az error null refrence
            CartItems = new List<CartItem>();
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            //JavaScriptSerializer() jahat tabdil cookie string to onject
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartitems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartitems)
            {
                item.TotalItemPrice = item.Count * item.UnitPrice;
            }

            CartItems = _productQuery.CheckInventoryStatus(cartitems);
        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(itemToRemove);
            var options = new CookieOptions { Expires = DateTime.Now.AddDays(2),IsEssential = true};
            Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
            return RedirectToPage("/Cart");
        }

        public IActionResult OnGetGoToCheckOut()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartitems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartitems)
            {
                item.TotalItemPrice = item.Count * item.UnitPrice;
            }

            CartItems = _productQuery.CheckInventoryStatus(cartitems);

            //if (cartitems.Any(x=>!x.IsInStock))
            //    return RedirectToPage("/Cart");
            //return RedirectToPage("Checkout");

            return RedirectToPage(cartitems.Any(x => !x.IsInStock) ? "/Cart" : "Checkout");
        }
    }
}
