using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountMangement.Domain.ColleagueDiscountAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository:RepositoryBase<long,ColleagueDiscount>, IColleagueDiscountRepository

    {
        private readonly DiscountContext _context;
        private readonly ShopContext _ShopContext;
        public ColleagueDiscountRepository(DiscountContext context,ShopContext shopContext) : base(context)
        {
            _context = context;
            _ShopContext = shopContext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _context.ColleagueDiscounts.Select(x => new EditColleagueDiscount
                {
                   Id = x.Id,
                   DiscountRate = x.DiscountRate,
                   ProductId = x.ProductId
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _ShopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                CrearionDate = x.CreationDate.ToFarsi(),
                DiscountRate = x.DiscountRate

            });
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(discounts =>
                discounts.Pruduct = products.FirstOrDefault(x => x.Id == discounts.ProductId)?.Name);
            return discounts;
        }
    }
}
