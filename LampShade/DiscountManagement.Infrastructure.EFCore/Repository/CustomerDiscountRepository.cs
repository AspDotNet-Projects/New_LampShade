using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using _0_Framework.Application;
using _0_Framework.Repository;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountMangement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository :RepositoryBase<long,CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext context,ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
        }


        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount
                {
                    Id = x.Id,
                    DiscountRate = x.DiscountRate,
                    StartDate = x.StartDate.ToString(),
                    EndDate = x.EndDate.ToString(),
                    Reason = x.Reason


                })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CostomerDiscountSearchModel search)
        {
            var product = _shopContext.Products.Select(x => new {x.Id, x.Name}).ToList();
           var query=_context.CustomerDiscounts.Select(x=>new CustomerDiscountViewModel
           {
               Id = x.Id,
               DiscountRate = x.DiscountRate,
               StartDate = x.StartDate.ToFarsi(),
               StartDateGr = x.StartDate,
               EndDate = x.EndDate.ToFarsi(),
               EndDateGr = x.EndDate,
               Reason = x.Reason

           });
           if (search.ProductId > 0)
               query = query.Where(x => x.ProductId == search.ProductId);
           if (!string.IsNullOrWhiteSpace(search.StartDate))
           {
               //ToGeorgianDateTime() Convert to Miladi date
                query = query.Where(x => x.StartDateGr > search.StartDate.ToGeorgianDateTime());
           }
           if (!string.IsNullOrWhiteSpace(search.EndDate))
           {
               
               query = query.Where(x => x.EndDateGr< search.EndDate.ToGeorgianDateTime());
           }

           var discounts = query.OrderByDescending(x => x.Id).ToList();
            //? baraye inke null bood khalil bashe
            discounts.ForEach(discounts
               =>discounts.Product=product.FirstOrDefault(x=>x.Id ==discounts.ProductId)?.Name);
            return discounts;
        }
    }
}
