using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;

using AccountManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository: RepositoryBase<long,Inventory> , IInventoryRepository
    {
        private readonly AccountContext _accountContext;
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopcontext;

        public InventoryRepository(InventoryContext inventoryContext,ShopContext shopContext, AccountContext accountContext) :base(inventoryContext)
        {
            _shopcontext = shopContext;
            _accountContext = accountContext;
            _inventoryContext = inventoryContext;
        }


        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                UnitePrice = x.UnitePrice,
                ProductId = x.ProductId

            }).FirstOrDefault(x => x.Id == id);
        }

        public Inventory GetBy(long productid)
        {
            return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productid);

        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _shopcontext.Products.Select(x => new {x.Id, x.Name}).ToList();

            var query = _inventoryContext.Inventory.Select(x => new InventoryViewModel
            {
                Id = x.Id,
                UnitePrice = x.UnitePrice,
                InStock = x.InStock,
                ProductId = x.ProductId,
                CurrentCount = x.CalculateCurrentCount(),
                CreationDate = x.CreationDate.ToFarsi()
                
            });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            //برای چک کردن موجودی در انبار
            if (searchModel.InStock)
            query = query.Where(x => !x.InStock);


            var inventory = query.OrderByDescending(x => x.Id).ToList();
            
            inventory.ForEach(item => {
                //? یعنی اگه مفدار نداشت مفدار خالی برگردونه که خطا نده
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            });

            return inventory;


        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryid)
        {
            var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.Fullname }).ToList();
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.Id == inventoryid);
            var operations= inventory.Operations.Select(x => new InventoryOperationViewModel
            {
                Id = x.Id,
                Description = x.Description,
                OrderId = x.OrderId,
                CurrentCount = x.CurrentCount,
                Count = x.Count,
                Operation = x.Operation,
                OperationDate = x.OperationDate.ToFarsi(),
                OperatorId = x.OperatorId

            }).OrderByDescending(x=>x.Id).ToList();

            foreach (var operation in operations)
            {
                operation.Operator = accounts.FirstOrDefault(x => x.Id == operation.OperatorId)?.Fullname;
            }
            return operations;
        }
    }
}
