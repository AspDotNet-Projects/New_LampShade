using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.ProductCategory
{
    public interface IProductCategoryApplication
    {
        /// <summary>
        /// OperationResult for control errore
        /// </summary>
        /// <param name="Command"></param>
        /// <returns></returns>
        OperationResult Create(CreateProductCategory Command);
        OperationResult Edite(EditeProductCategory Command);
        EditeProductCategory GetDatails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

        
    }
}
