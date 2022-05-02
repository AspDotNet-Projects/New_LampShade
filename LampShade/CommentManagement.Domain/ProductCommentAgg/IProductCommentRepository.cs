using System.Collections.Generic;
using _0_Framework.Domain;
using CommentManagement.Application.Contract.ProductComment;

namespace CommentManagement.Domain.ProductCommentAgg
{
    public interface  IProductCommentRepository: IRepository<long,ProductComment>
    {
        List<ProductCommentViewModel> Search(ProductCommentSearchModel searchModel);
    }
}
