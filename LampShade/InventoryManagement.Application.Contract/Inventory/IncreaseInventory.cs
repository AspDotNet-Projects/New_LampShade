namespace InventoryManagement.Application.Contract.Inventory
{
    public class IncreaseInventory
    {
        //کدام اینونتوری افزایش داشته باش
        public long InventoryId { get; set; }
        public long Count { get; set; }
        public string Description { get; set; }
        
    }
}
