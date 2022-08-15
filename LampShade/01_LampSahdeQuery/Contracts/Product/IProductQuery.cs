using System.Collections.Generic;
using _01_LampSahdeQuery.Contracts.Product;
using ShopManagement.Application.Contract.Order;

namespace _01_LampShadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProductDetails(string slug);
        //آخرین محصولات
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
        List<CartItem> CheckInventoryStatus(List<CartItem> cartitems);

    }
}
