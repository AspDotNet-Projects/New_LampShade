using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication:IProductPictureApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            //if (_productPictureRepository.Exists(x => x.Picture == command.Picture && x.ProductId == command.ProductId))
            //    return operation.Failed(ApplicationMesseges.DuplicatedRecored);

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var path = $"{product.Category.Slug}//{product.Slug}";
            var picturepath = _fileUploader.Upload(command.Picture, path);
            var ProductPicture = new ProductPicture(command.ProductId, picturepath, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.Create(ProductPicture);
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var productpictre = _productPictureRepository.GetWithProductAngCategory(command.Id);
            if (productpictre==null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);

            var path = $"{productpictre.Product.Category.Slug}//{productpictre.Product.Slug}";
            var picturepath = _fileUploader.Upload(command.Picture, path);

            productpictre .Edit(command.ProductId, picturepath, command.PictureAlt,
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
