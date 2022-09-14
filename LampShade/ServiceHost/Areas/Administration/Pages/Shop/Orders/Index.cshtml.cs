using System.Collections.Generic;
using _0_Framework.Infrastructure;
using AccountManagement.Applicatoin.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Orders
{
    public class IndexModel : PageModel
    {
        public OrderSearchModel SearchModel;
        public SelectList Accounts;
        public List<OrderViewModel> Orders;

        private readonly IOrderApplication _orderApplication;
        private readonly IAccountApplication _accountApplication;


        public IndexModel(IOrderApplication orderApplication,IAccountApplication accountApplication)
        {
            _orderApplication = orderApplication;
            _accountApplication = accountApplication;
        }

        
        [NeedsPermission(ShopPermissions.ListProductCategories)]
        public void OnGet(OrderSearchModel searchModel)
        {
            Accounts =new SelectList(_accountApplication.GetAccounts(),"Id","Fullname");
            Orders = _orderApplication.Search(searchModel);
        }
    }
}
