using System.Collections.Generic;
using _01_LampSahdeQuery.Contracts.Product;

namespace _01_LampShadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        //آخرین محصولات
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
