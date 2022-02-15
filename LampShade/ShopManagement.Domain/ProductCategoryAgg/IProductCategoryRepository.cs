using ShopManagement.Application.Contract.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface  IProductCategoryRepository
    {
        void Create(ProductCategory intity);
        ProductCategory Get(long id);
        List<ProductCategory> GetAll();
        bool Exists(Expression<Func<ProductCategory, bool>> expression);
        EditeProductCategory GetDatails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        void SaveChange();
    }
}
