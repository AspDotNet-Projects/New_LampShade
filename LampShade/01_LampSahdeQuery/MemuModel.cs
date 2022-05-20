using System.Collections.Generic;
using _01_LampSahdeQuery.Contracts.ProductCategory;
using _01_LampShadeQuery.Contracts.ArticleCategory;

namespace _01_LampShadeQuery
{
    public class MemuModel
    {
        public List<ArticleCategoryQueryModel> articleCategories { get; set; }
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }

}
}
