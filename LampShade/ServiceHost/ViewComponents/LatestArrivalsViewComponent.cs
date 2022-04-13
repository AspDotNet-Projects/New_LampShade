using _01_LampSahdeQuery.Contracts.ProductCategory;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent : ViewComponent
    {
        private readonly IProductQuery _productQuery;

        public LatestArrivalsViewComponent(IProductQuery product)
        {
            _productQuery = product;
        }


        public IViewComponentResult Invoke()
        {
            var Product = _productQuery.GetLatestArrivals();
            return View(Product);
        }
    }
}
