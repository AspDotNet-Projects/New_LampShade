using System.Collections.Generic;
using System.Text.RegularExpressions;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public long AccountId { get; set; }
        public double TotalAmount { get; private set; }
        public double DiscountAmount{ get; private set; }
        public double PayAmount { get; private set; }
        
        //pardakht shodde ya na
        public bool IsPaid { get; private set; }
        public bool IsCanceled{ get; private set; }
        
        //shomare paygiri sefaresh
        public string IssueTrackingNo { get; private set; }


        //shmare paygiri kharid dar dargah pardakht ke bank mide va mal mast
        public long RefId { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(long accountId, double totalAmount, double discountAmount, double payAmouont, string issueTrackingNo, List<OrderItem> items)
        {
            AccountId = accountId;
            TotalAmount = totalAmount;
       
            DiscountAmount = discountAmount;
            PayAmount = payAmouont;
            IssueTrackingNo = issueTrackingNo;
            Items = items;
            IsCanceled = false;
            IsPaid = false;
            //dar ebteda sefr ast
            RefId = 0;
            items = new List<OrderItem>();
        }

        public void PaymentSucceeded(long refid)
        {
            IsPaid = true;
            if (refid != 0)
                RefId = refid;
        }

        public void SetIssueTrackingNo(string number)
        {
            IssueTrackingNo = number;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }
    }
}
