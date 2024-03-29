using System.Collections.Generic;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategories;
        private readonly IProductCategoryApplication _productCategories;
        /// <summary>
        /// jahat estefade dar Index.cshtml
        /// </summary>
        public ProductCategorySearchModel SearchModel;

        public IndexModel(IProductCategoryApplication productCategories)
        {
            _productCategories = productCategories;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>-->paramer vorodi jahat search hast ke zaman search ejra mishe
        /// zaman load safhe searchModel Null hast var All barmigarde dar ----------> ProductCategoryRepository
        /// -------      if (!string.IsNullOrWhiteSpace(searchModel.Name))
        ///              query = query.Where(x => x.Name.Contains(searchModel.Name));
        ///
        [NeedsPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(ProductCategorySearchModel searchModel)
        {

            ProductCategories = _productCategories.Search(searchModel);
        }

        /// <summary>
        /// Baraye ShowModal
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }
        [NeedsPermission(ShopPermissions.ListProductCategories)]
        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategories.Create(command);
            return new JsonResult(result);
        }

        
        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategories.GetDatails(id);
            return Partial("./Edit", productCategory);
        }

        [NeedsPermission(ShopPermissions.EditProductCategory)]
        public JsonResult OnPostEdit(EditeProductCategory command)
        {
            var result = _productCategories.Edite(command);
            return new JsonResult(result);
        }
    }
}
