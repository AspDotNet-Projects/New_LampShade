using System.Collections.Generic;
using BlogManagement.Application.Contracts.ArticleCategory;


namespace _01_LampShadeQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetArticleCategory(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}
