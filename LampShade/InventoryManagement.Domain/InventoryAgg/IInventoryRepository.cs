using System.Collections.Generic;
using _0_Framework.Domain;
using InventoryManagement.Application.Contract.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long, Inventory>
    {
        EditInventory GetDetails(long id);
        /// <summary>
        /// برای گرفبن مشخصات انبار بر اساس کد کالا
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        Inventory GetBy(long productid);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
