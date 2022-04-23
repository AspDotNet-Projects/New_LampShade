using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository:IRepository<long,Product>
    {
        Product GetProductWithCategory(long id);
        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();
        List<ProductViewModel> search(ProductSearchModel searchModel);
    }
}
