using System.Collections.Generic;
using _0_Framework.Application;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public interface IArticleCategoryApplication
    {
        OperationResult Create(CreateArticleCategory command);
        EditArticleCategory GetDetails(long id);
        OperationResult Edit(EditArticleCategory command);
        List<ArticleCategoryViewModel> GetArticleCategories();

        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}
