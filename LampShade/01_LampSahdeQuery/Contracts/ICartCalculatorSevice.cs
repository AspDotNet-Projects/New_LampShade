using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Update;
using ShopManagement.Application.Contract.Order;

namespace _01_LampShadeQuery.Contracts
{
    public interface ICartCalculatorSevice
    {
        Cart ComputeCart(List<CartItem> cartItems);
    }
}
