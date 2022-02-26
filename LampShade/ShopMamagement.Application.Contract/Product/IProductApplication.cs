using System.Collections.Generic;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.Product
{
    public interface  IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);

        OperationResult InStock(long id);
        OperationResult NotInStock(long id);

        EditProduct Getdetails(long id);

        List<ProductViewModel> GetProducts();
        List<ProductViewModel> Search(ProductSearchModel searchModel);

    }
}
