using _01_LampSahdeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductViewComponent : ViewComponent
    {
        //اکه نام ViewComponent
        //درست نوشته نشه خطا داریم
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryWithProductViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var category = _productCategoryQuery.GetProductCategoriesWithProducts();
            //داخل کامپوننت یک کامپوننت با همین نام ایجاد می کنیم
            //ProductCategoryWithProduct
            //که در واقع نام کلاس است
            return View(category);
        }
    }
}
