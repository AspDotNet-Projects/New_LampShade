using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository:IProductCategoryRepository
    {
        private readonly ShopContext _shopContext;

        public ProductCategoryRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public void Create(ProductCategory intity)
        {
            _shopContext.ProductCategories.Add(intity);
        }

        public ProductCategory Get(long id)
        {
            return _shopContext.ProductCategories.Find(id);
        }

        public List<ProductCategory> GetAll()
        {
            return _shopContext.ProductCategories.ToList();
        }

        public bool Exists(Expression<Func<ProductCategory, bool>> expression)
        {
            /// expression is shart
            return _shopContext.ProductCategories.Any(expression);
        }

        public EditeProductCategory GetDatails(long id)
        {
            return _shopContext.ProductCategories.Select(x => new EditeProductCategory()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _shopContext.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToString(),
                Picture = x.Picture
            });
            //baraye zamani ke search khali bood va batyad all search ya ba name bashad
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public void SaveChange()
        {
            _shopContext.SaveChanges();
        }
    }
}
