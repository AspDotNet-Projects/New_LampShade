using System.Collections.Generic;


namespace _01_LampShadeQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}
