namespace DiscountManagement.Application.Contract.ColleagueDiscount
{
    public class ColleagueDiscountViewModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string Pruduct { get; set; }
        public int DiscountRate { get; set; }
        public string CrearionDate { get; set; }
    }
}