using System.Collections.Generic;
using System.Linq;
using _0_Framework.Repository;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository: RepositoryBase<long,Inventory> , IInventoryRepository
    {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopcontext;

        public InventoryRepository(InventoryContext inventoryContext,ShopContext shopContext) :base(inventoryContext)
        {
            _shopcontext = shopContext;
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
                CurrentCount = x.CalculateCurrentCount()
                
            });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            //برای چک کردن موجودی در انبار
            if (!searchModel.InStock)
            query = query.Where(x => !x.InStock);


            var inventory = query.OrderByDescending(x => x.Id).ToList();
            
            inventory.ForEach(item => {
                //? یعنی اگه مفدار نداشت مفدار خالی برگردونه که خطا نده
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            });

            return inventory;


        }
    }
}
