namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    public class CostomerDiscountSearchModel
    {
        public long ProductId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}