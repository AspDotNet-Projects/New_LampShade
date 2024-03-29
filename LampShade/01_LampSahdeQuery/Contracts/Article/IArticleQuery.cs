﻿using System.Collections.Generic;

namespace _01_LampShadeQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        List<ArticleQueryModel> LatestArticle();
        ArticleQueryModel GetArticleDetails(string slug);
    }
}
