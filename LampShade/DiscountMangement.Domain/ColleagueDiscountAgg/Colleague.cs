using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace DiscountMangement.Domain.ColleagueDiscountAgg
{
    public class Colleague : EntityBase
    {
        public long  ProductId { get;private set; }
        public int DiscountRate { get; private set; }
        public bool IsRemove { get; private set; }

        public Colleague(long productId, int discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            IsRemove = false;
        }

        public void Edit(long productId, int discountRate)
        {
            ProductId = productId;
            DiscountRate = discountRate;
        }

        public void Remove()
        {
            IsRemove = true;
        }

        public void Restore()
        {
            IsRemove = false;
        }
    }
}
