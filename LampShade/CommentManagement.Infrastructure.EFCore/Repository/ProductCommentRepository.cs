using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Repository;
using CommentManagement.Application.Contract.ProductComment;
using CommentManagement.Domain.ProductCommentAgg;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class ProductCommentRepository :RepositoryBase<long,ProductComment>,IProductCommentRepository
    {
        private readonly CommentContext _context;
        public ProductCommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductCommentViewModel> Search(ProductCommentSearchModel searchModel)
        {
            var query= _context.ProductComments.Include(x => x.Product)
                .Select(x => new ProductCommentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Message = x.Message,
                    ProductId = x.ProductId,
                    Email = x.Email,
                    IsCanceled = x.IsCanceled,
                    IsConfirm = x.IsConfirmed,
                    ProductName = x.Product.Name,
                    CommentDate = x.CreationDate.ToFarsi()
                });
            if(!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Email))
            {
                query = query.Where(x => x.Email.Contains(searchModel.Email));
            }
            return query.OrderByDescending(x=>x.Id).ToList();
        }
    }
}
