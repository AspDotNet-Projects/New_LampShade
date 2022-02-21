using System.Collections.Generic;
using System.Text.Json.Serialization;
using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication:IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory Command)
        {
            var operationresult = new OperationResult();
            if (_productCategoryRepository.Exists(x => x.Name == Command.Name))
                return operationresult.Failed(ApplicationMesseges.DuplicatedRecored);
            
            
            
                var Slug = Command.Slug.Slugify();

               
                var productcategory = new ProductCategory(Command.Name, Command.Description, Command.Picture
                    , Command.PictureAlt, Command.PictureTitle, Command.Keywords, Command.MetaDescription
                    , Slug);
                _productCategoryRepository.Create(productcategory);
                _productCategoryRepository.SaveChange();

                return operationresult.Succedded();

        }

        public OperationResult Edite(EditeProductCategory Command)
        {
            var operationresult = new OperationResult();
            var productcategory = _productCategoryRepository.Get(Command.Id);

            ///null bood
            if (productcategory == null)
                return operationresult.Failed(ApplicationMesseges.RecoredNotFound);
            
           
            if (_productCategoryRepository.Exists(x=>x.Name== Command.Name && x.Id !=Command.Id))
                return  operationresult.Failed(ApplicationMesseges.DuplicatedRecored);
            
            var Slug=Command.Slug.Slugify();

            productcategory.Edite(Command.Name,Command.Description,Command.Picture
            ,Command.PictureAlt,Command.PictureTitle,Command.Keywords
            ,Command.MetaDescription,Slug);
            _productCategoryRepository.SaveChange();

            return operationresult.Succedded();

        }

        public EditeProductCategory GetDatails(long id)
        {
            return _productCategoryRepository.GetDatails(id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }
    }
}
