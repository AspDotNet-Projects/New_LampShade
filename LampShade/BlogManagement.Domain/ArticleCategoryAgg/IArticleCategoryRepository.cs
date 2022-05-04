using System.Collections.Generic;
using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long,ArticleCategory>
    {


        EditArticleCategory GetDetails(long id);
        /// <summary>
        /// در اینجا ما دامین را با اپلیکیشن کانترکت مرتبط میکنیم که
        /// اشکالی نداره به این دلیل که هیچ پیاده سازی داخل کانترکت وجود
        /// نداره و تمام پیاده سازی ها داخل اپلیکیشن هست
        /// و اگر پیاده سازی با دامین در ارتباط نباشه
        /// قانون معماری پیازی حفظ خواخد شد
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}
