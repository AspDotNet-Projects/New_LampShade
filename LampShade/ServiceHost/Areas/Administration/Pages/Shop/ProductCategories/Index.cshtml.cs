using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategories;
        private readonly IProductCategoryApplication _productCategories;

        public IndexModel(IProductCategoryApplication productCategories)
        {
            _productCategories = productCategories;
        }

        public void OnGet(ProductCategorySearchModel searchModel)
        {

            ProductCategories=_productCategories.Search(searchModel);
        }
    }
}
