using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contract.Order;

namespace _01_LampShadeQuery.Contracts
{
    public class CartCalculatorSevice : ICartCalculatorSevice
    {
        public readonly IAuthHelper _AuthHelper;
        private readonly DiscountContext _discountContext;

        public CartCalculatorSevice(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _AuthHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                //ahe isremove false bood
                .Where(x=>!x.IsRemove)
                .Select(x=>new {x.DiscountRate,x.ProductId})
                .ToList();
            var customerDiscounts = _discountContext.CustomerDiscounts
                //ahe isremove false bood
                .Where(x =>x.StartDate<DateTime.Now  &&  x.EndDate>DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId })
                .ToList();
            //gereftan rol mojod
            var currentAccountRoles = _AuthHelper.CurrentAccountRole();
            if (currentAccountRoles == Roles.Colleague)
            {
                foreach (var cartitem in cartItems)
                {
                    var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == cartitem.Id);
                    //age null bod boro be mahsol badi dar edame foreach
                    if(colleagueDiscount==null)  continue;
                    cartitem.DiscountRate = colleagueDiscount.DiscountRate;
                    cartitem.DiscouontAmmunt = ((cartitem.TotalItemPrice * cartitem.DiscountRate)/100);
                    cartitem.ItemPayAmount = cartitem.TotalItemPrice - cartitem.DiscouontAmmunt;
                    //ezafe kardan Item ha be list
                    cart.Add(cartitem);

                }
            }
            else
            {

                foreach (var cartitem in cartItems)
                {
                    var customerdiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == cartitem.Id);
                    //age null bod boro be mahsol badi dar edame foreach
                    if (customerdiscount == null) continue;
                    cartitem.DiscountRate = customerdiscount.DiscountRate;
                    cartitem.DiscouontAmmunt = ((cartitem.TotalItemPrice * cartitem.DiscountRate) / 100);
                    cartitem.ItemPayAmount = cartitem.TotalItemPrice - cartitem.DiscouontAmmunt;
                    //ezafe kardan Item ha be list
                    cart.Add(cartitem);

                }
            }
        

            return cart;
        }
    }
}