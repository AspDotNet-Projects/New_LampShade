using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();

            if(_productRepository.Exists(x=>x.Name==command.Name))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);

            var slug = command.Slug.Slugify();
            var product = new Product(command.Name,command.Code,command.UnitPrice,
                command.ShortDescription,command.Description,command.Picture,
                command.PictureAlt,command.PictureTitle,command.CategoryId,
                slug, command.Keywords,command.MetaDescription);
            _productRepository.Create(product);
            _productRepository.SaveChange();
            return operation.Succedded();

        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(command.Id);

            if (product == null)
                operation.Failed(ApplicationMesseges.RecoredNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id==command.Id))
                operation.Failed(ApplicationMesseges.DuplicatedRecored);
            var slug = command.Slug.Slugify();
            product.Edite(command.Name, command.Code, command.UnitPrice,
                command.ShortDescription, command.Description, command.Picture,
                command.PictureAlt, command.PictureTitle, command.CategoryId,
                slug, command.Keywords, command.MetaDescription);
            _productRepository.SaveChange();
            return operation.Succedded();

        }

        public OperationResult InStock(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);

            if (product == null)
                operation.Failed(ApplicationMesseges.RecoredNotFound);

            product.InStock();
            _productRepository.SaveChange();
            return operation.Succedded();
        }

        public OperationResult NotInStock(long id)
        {
            var operation = new OperationResult();
            var product = _productRepository.Get(id);

            if (product == null)
                operation.Failed(ApplicationMesseges.RecoredNotFound);

            product.NotInStock();
            _productRepository.SaveChange();
            return operation.Succedded();
        }

        public EditProduct Getdetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.search(searchModel).ToList();
        }
    }
}
