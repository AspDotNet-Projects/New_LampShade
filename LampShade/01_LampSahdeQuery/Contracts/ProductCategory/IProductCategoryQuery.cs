using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampSahdeQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> getProductCategories();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}
