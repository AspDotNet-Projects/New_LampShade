namespace InventoryManagement.Application.Contract.Inventory
{
    public class ReduceInventory
    {
        public long InventoryId { get; set; }
        //برای اینکه کاربر یا اپراتور بتونه مقدار رو کم بکنه
        public long ProdductId { get; set; }
        public long Count { get;  set; }
        public string Description { get; set; }
        public long OrderId{ get; set; }
    }
}