using System.Collections.Generic;
using _0_Framework.Application;

namespace CommentManagement.Application.Contract.ProductComment
{
    public interface IProductCommentApplication
    {
        OperationResult Add(AddProductComment command);
        OperationResult Confirm(long id);
        OperationResult Cancel(long id);
        List<ProductCommentViewModel> Search(ProductCommentSearchModel searchModel);



    }
}
