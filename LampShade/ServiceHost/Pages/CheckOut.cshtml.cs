using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_LampShadeQuery.Contracts;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages
{
    /// <summary>
    /// hatman login karde bashe
    /// </summary>
    [Authorize]
    public class CheckOutModel : PageModel
    {
        public Cart Cart;
        public const string CookieName = "cart-items";
        /// <summary>
        /// be inkar Inject goyand
        /// </summary>
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IAuthHelper _authHelper;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorSevice _calculatorSevice;

        public CheckOutModel(ICartCalculatorSevice calculatorSevice, ICartService cartService
            ,IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory, IAuthHelper authHelper)
        {
            _calculatorSevice = calculatorSevice;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
            _authHelper = authHelper;
            Cart = new Cart();
        }

        public void OnGet()
        {
            //JavaScriptSerializer() jahat tabdil cookie string to onject
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            if (value == null)
                 RedirectToPage("/Index");
            var cartitems = serializer.Deserialize<List<CartItem>>(value);
            foreach (var item in cartitems)
                //calc total price in Contract 
                item.CalculateTotalItemPrice();
            Cart = _calculatorSevice.ComputeCart(cartitems);



            //set kardan cartItem to singletone
            _cartService.Set(Cart);
        }

        public IActionResult OnPostPay()
        {
            var cart = _cartService.Get();
            var result = _productQuery.CheckInventoryStatus(cart.Items);
            //age har kodam dar anbar nabod false beshe
            //Or   if (result.Any(x => x.IsInStock == false))
            if (result.Any(x => !x.IsInStock))
                    return RedirectToPage("/Cart");

            var orderID = _orderApplication.PlaceOrder(cart);

            var accountUserName = _authHelper.CurrentAccountInfo().UserName;
            var mobile = _authHelper.CurrentAccountInfo().Mobile;

            var paymentResponse = _zarinPalFactory.CreatePaymentRequest(cart.PayAmount.ToString(), mobile, "", "?????? ?? ???? ???? www.CG.ir ????? ?? ???.",
                orderID);

            return Redirect(
                $"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }

        public IActionResult OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {
            var orderAmount = _orderApplication.GetAmountBy(oId);
            var verificationResponse = _zarinPalFactory.CreateVerificationRequest(authority, orderAmount.ToString(CultureInfo.InvariantCulture));

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status == 100)
            {
                var issueTrackingNo=_orderApplication.PaymentSuccedded(oId, verificationResponse.RefID);
                Response.Cookies.Delete("cart-items");
                result = result.Succeeded("Å—œ«Œ  »« „Ê›ﬁ?  «‰Ã«„ ‘œ.", issueTrackingNo);
                return RedirectToPage("/PaymentResult",result);
            }

            
                result = result.Failed(
                    "Å—œ«Œ  »« „Ê›ﬁ?  «‰Ã«„ ‰‘œ. œ— ’Ê—  ò”— ÊÃÂ «“ Õ”«» °„»·€ œ— „œ  24 ”«⁄  »Â Õ”«» ‘„« Ê«—?“ ŒÊ«Âœ ‘œ.");
                return RedirectToPage("/PaymentResult", result);
           
        }
    }
}
