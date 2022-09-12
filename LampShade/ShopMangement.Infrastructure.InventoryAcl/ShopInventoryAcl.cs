using ShopManagement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using ShopManagement.Domain.OrderAgg;
using InventoryManagement.Application.Contract.Inventory;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl: IShopInventoryAcl
    {
        private readonly IInventoryApplication _iinventoryApplication;

        public ShopInventoryAcl(IInventoryApplication iinventoryApplication)
        {
            _iinventoryApplication = iinventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            //var command = new List<ReduceInventory>();
            //foreach (var orderItem in items)
            //{
            //    var item = new ReduceInventory(orderItem.ProductId, orderItem.Count,"خرید مشتری", orderItem.OrderId);
            //    command.Add(item);
            //}
            //YA
            var command = items.Select(orderItem => new ReduceInventory(orderItem.ProductId, orderItem.Count, "خرید مشتری", 
                orderItem.OrderId)).ToList();

            //alave bar reduce iscucces ro ham migire
            return _iinventoryApplication.Reduce(command).IsSuccedded;
        }
    }
}
