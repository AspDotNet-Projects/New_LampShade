using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    /// <summary>
    /// ProductCategoryRepository-->RepositoryBase
    /// </summary>
    public class ProductCategoryRepository:RepositoryBase<long,ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _shopContext;

        public ProductCategoryRepository(ShopContext shopContext) :base(shopContext)
        {
            _shopContext = shopContext;
        }


        public List<ProductCategoryViewModel> GetProductCategories_selectlist()
        {
            return _shopContext.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public EditeProductCategory GetDatails(long id)
        {
            return _shopContext.ProductCategories.Select(x => new EditeProductCategory()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    //Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public string GetSlugById(long id)
        {
            return _shopContext.ProductCategories.Select(x => new {x.Id, x.Slug}).FirstOrDefault(x => x.Id == id).Slug;
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _shopContext.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi(),
                Picture = x.Picture
            });
            //baraye zamani ke search khali bood va batyad all search ya ba name bashad
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.Id).ToList();
        }


    }
}
