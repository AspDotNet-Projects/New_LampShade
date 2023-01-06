namespace _01_LampShadeQuery.Contracts.Inventory
{
    public class ProductCount
    {
        public string Product { get; set; }
        public long ProductId { get; set; }
        public double UnitePrice { get; set; }
        public long CurrentCount { get; set; }
        public bool InStock { get; set; }
    }
}
