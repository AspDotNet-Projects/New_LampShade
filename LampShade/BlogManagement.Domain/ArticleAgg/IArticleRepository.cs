﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using BlogManagement.Application.Contracts.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository:IRepository<long,Article>
    {
        EditArticle Getdetails(long id);
        Article GetWithCatgegory(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
