using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication:IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);
            var ProductPicture = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.Create(ProductPicture);
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productpictre = _productPictureRepository.Get(command.Id);
            if (productpictre==null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);
            if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId && x.Id!=command.Id))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);

            productpictre .Edit(command.ProductId, command.Picture, command.PictureAlt,
                command.PictureTitle);
            
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productpictre = _productPictureRepository.Get(id);
            if (productpictre == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);
           
            productpictre.Remove();

            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productpictre = _productPictureRepository.Get(id);
            if (productpictre == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);

            productpictre.Restore();

            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDatails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
