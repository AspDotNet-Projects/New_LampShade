using InventoryManagement.Application.Contract.Inventory;

namespace _01_LampShadeQuery.Contracts.Inventory
{
    public interface IInventoryQuery
    {
        StockStatus CheckStock(IsInStock command);
    }
}
