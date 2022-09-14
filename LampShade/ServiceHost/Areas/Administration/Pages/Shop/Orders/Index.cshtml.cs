using System.Collections.Generic;
using System.Security.Cryptography.Xml;
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

        
 
        public void OnGet(OrderSearchModel searchModel)
        {
            Accounts =new SelectList(_accountApplication.GetAccounts(),"Id","Fullname");
            Orders = _orderApplication.Search(searchModel);
        }

        public IActionResult OnGetConfirm(long id)
        {
            //refid=0 chon dasti darim anjam midim
            _orderApplication.PaymentSuccedded(id, 0);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetCancel(long id)
        {
            _orderApplication.Cancel(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetItems(long id)
        {
            var items = _orderApplication.GetItems(id);
            return Partial("Items", items);
        }
    }
}
