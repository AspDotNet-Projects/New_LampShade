using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        [TempData] 
        public string Messege { get; set; }
        public List<ProductViewModel> Products;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public SelectList Productcategories;
        /// <summary>
        /// jahat estefade dar Index.cshtml
        /// </summary>
        public ProductSearchModel SearchModel;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>-->paramer vorodi jahat search hast ke zaman search ejra mishe
        /// zaman load safhe searchModel Null hast var All barmigarde dar ----------> ProductCategoryRepository
        /// -------      if (!string.IsNullOrWhiteSpace(searchModel.Name))
        ///              query = query.Where(x => x.Name.Contains(searchModel.Name));
        ///+
        /// 
        [NeedsPermission(ShopPermissions.ListProducts)]
        public void OnGet(ProductSearchModel searchModel)
        {
            Productcategories = new SelectList(_productCategoryApplication.GetProductcategory_selectlist(), "Id", "Name");

            Products = _productApplication.Search(searchModel);
        }

        /// <summary>
        /// Baraye ShowModal
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct();
            command.Categories = _productCategoryApplication.GetProductcategory_selectlist();
            return Partial("./Create", command);
        }

        [NeedsPermission(ShopPermissions.CreateProducts)]
        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

      
        public IActionResult OnGetEdit(long id)
        {
            var product = _productApplication.Getdetails(id);
            product.Categories = _productCategoryApplication.GetProductcategory_selectlist();
            return Partial("./Edit", product);
        }

        [NeedsPermission(ShopPermissions.EditProduct)]
        public JsonResult OnPostEdit(EditProduct command)
            {
            var result = _productApplication.Edit(command);
            return new JsonResult(result);
        }




    }
}
