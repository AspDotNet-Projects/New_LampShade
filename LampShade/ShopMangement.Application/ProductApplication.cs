using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();

            if(_productRepository.Exists(x=>x.Name==command.Name))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);

            var slug = command.Slug.Slugify();
            var categoryslug = _productCategoryRepository.GetSlugById(command.CategoryId);
            //به این خاطر که همه محصولات امکان دارد محصولات متنوعی داسته باشند یا این کار به ازای همه گروهن های محصوالات طبقع بندی میکنیم
            var path = $"{categoryslug}//{slug}";
            var picturepath = _fileUploader.Upload(command.Picture, path);
            var product = new Product(command.Name,command.Code,
                command.ShortDescription,command.Description, picturepath,
                command.PictureAlt,command.PictureTitle,command.CategoryId,
                slug, command.Keywords,command.MetaDescription);
            _productRepository.Create(product);
            _productRepository.SaveChange();
            return operation.Succedded();

        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);

            if (product == null)
                operation.Failed(ApplicationMesseges.RecoredNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id==command.Id))
                operation.Failed(ApplicationMesseges.DuplicatedRecored);
            var slug = command.Slug.Slugify();
            //به این خاطر که همه محصولات امکان دارد محصولات متنوعی داسته باشند یا این کار به ازای همه گروهن های محصوالات طبقع بندی میکنیم
            var path = $"{product.Category.Slug}/{slug}";
            var picturepath = _fileUploader.Upload(command.Picture, path);

            product.Edite(command.Name, command.Code, 
                command.ShortDescription, command.Description, picturepath,
                command.PictureAlt, command.PictureTitle, command.CategoryId,
                slug, command.Keywords, command.MetaDescription);
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
