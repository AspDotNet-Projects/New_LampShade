namespace InventoryManagement.Application.Contract.Inventory
{
    /// <summary>
    /// یعنی سرچ بر اساس کد کالا یا موجود بودن یا عدم وجود آن در انبار
    /// </summary>
    public class InventorySearchModel
    {
        public long ProductId { get; set; }
        public bool InStock { get; set; }
    }
}