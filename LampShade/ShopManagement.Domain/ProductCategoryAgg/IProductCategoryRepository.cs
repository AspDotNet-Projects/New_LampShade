using ShopManagement.Application.Contract.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using _0_Framework.Domain;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    /// <summary>
    ///  IProductCategoryRepository---->IRepository
    /// </summary>
    public interface  IProductCategoryRepository:IRepository<long,ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories_selectlist();
       EditeProductCategory GetDatails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);

    }
}
