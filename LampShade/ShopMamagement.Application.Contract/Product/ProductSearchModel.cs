namespace ShopManagement.Application.Contract.Product
{
    /// <summary>
    /// Search By Name and code and Categoryid
    /// </summary>
    public class ProductSearchModel
    {

        public string Name { get; set; }
        public string Code { get; set; }
        public long CategoryId { get; set; }
    }
}