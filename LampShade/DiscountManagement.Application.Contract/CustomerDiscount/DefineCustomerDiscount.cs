﻿namespace DiscountManagement.Application.Contract.CustomerDiscount
{
    /// <summary>
    /// تعریف تخفیف برای مشتری
    /// </summary>
    public class DefineCustomerDiscount
    {
        public long ProductId { get;  set; }
        public int DiscountRate { get;  set; }
        public string StartDate { get;  set; }
        public string EndDate { get;  set; }
        public string Reason { get;  set; }
    }
}