using System.Collections.Generic;
using _0_Framework.Application;

namespace BlogManagement.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        OperationResult Creaate(CreateArticle commnak);
        OperationResult Edit(EditArticle command);
        EditArticle Getdetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
