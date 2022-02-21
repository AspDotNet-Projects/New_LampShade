using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository:IRepository<long,Product>
    {
        EditProduct GetDetails(long id);
        List<ProductSearchModel> search(ProductSearchModel searchModel);
    }
}
