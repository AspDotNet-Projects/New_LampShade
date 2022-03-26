using System.Collections.Generic;
using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CustomerDiscountSearchModel SearchModel;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>-->paramer vorodi jahat search hast ke zaman search ejra mishe
        /// zaman load safhe searchModel Null hast var All barmigarde dar ----------> ProductCategoryRepository
        /// -------      if (!string.IsNullOrWhiteSpace(searchModel.Name))
        ///              query = query.Where(x => x.Name.Contains(searchModel.Name));
        /// 
        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");

            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }
        
        /// <summary>
        /// Baraye ShowModal
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProducts()
            };

            return Partial("./Create",command);
        }

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _customerDiscountApplication.GetDetails(id);
            customerDiscount.Products =_productApplication.GetProducts();
            return Partial("./Edit", customerDiscount);
        }

        public JsonResult OnPostEdit(EditCustomerDiscount command)
            {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }


    }
}
