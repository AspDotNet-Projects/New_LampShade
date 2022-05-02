using System.Collections.Generic;
using _0_Framework.Application;
using CommentManagement.Application.Contract.ProductComment;
using CommentManagement.Domain.ProductCommentAgg;

namespace CommentManagement.Application
{
    public class ProductCommentApplication : IProductCommentApplication
    {
        private readonly IProductCommentRepository _productCommentRepository;

        public ProductCommentApplication(IProductCommentRepository productCommentRepository)
        {
            _productCommentRepository = productCommentRepository;
        }

        public OperationResult Add(AddProductComment command)
        {
            var operation = new OperationResult();
            var comment = new ProductComment(command.Name, command.Email, command.Message, command.ProductId);
            _productCommentRepository.Create(comment);
            _productCommentRepository.SaveChange();
            return operation.Succedded();
        }

        public OperationResult Confirm(long id)
        {
            var operation = new OperationResult();
            var comment = _productCommentRepository.Get(id);
            
            if (comment == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);

            comment.Confirm();
            _productCommentRepository.SaveChange();
            return operation.Succedded();

        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();
            var comment = _productCommentRepository.Get(id);

            if (comment == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);

            comment.Cancel();
            _productCommentRepository.SaveChange();
            return operation.Succedded();
        }

        public List<ProductCommentViewModel> Search(ProductCommentSearchModel searchModel)
        {
            return _productCommentRepository.Search(searchModel);
        }
    }
}
