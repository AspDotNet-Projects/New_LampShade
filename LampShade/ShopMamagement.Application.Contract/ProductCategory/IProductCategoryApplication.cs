using System.Collections.Generic;

namespace ShopMamagement.Application.Contract.ProductCategory
{
    public interface IProductCategoryApplication
    {
        void Create(CreateProductCategory Command);
        void Edite(EditeProductCategory Command);
        ShopManagement.Domain.ProductCategoryAgg.ProductCategory GetDatails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

        
    }
}
