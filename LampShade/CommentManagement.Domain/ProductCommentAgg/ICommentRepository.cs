using System.Collections.Generic;
using _0_Framework.Domain;
using CommentManagement.Application.Contract.ProductComment;

namespace CommentManagement.Domain.ProductCommentAgg
{
    public interface  ICommentRepository: IRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}
