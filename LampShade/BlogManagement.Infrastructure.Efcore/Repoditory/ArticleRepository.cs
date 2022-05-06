﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Repository;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.Efcore.Repoditory
{
    public class ArticleRepository:RepositoryBase<long,Article>,IArticleRepository
    {
        private readonly BlogContext _context;
        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public EditArticle Getdetails(long id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                Description=x.Description,
                ShortDescription = x.ShortDescription,
                Title = x.Title,
                Slug = x.Slug,
                MetaDescription = x.MetaDescription,
                PublishDate = x.PublishDate.ToFarsi(),
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Keywords = x.Keywords,
                CanonicalAddress = x.CanonicalAddress,
                CategoryId = x.CategoryId

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
           var query=_context.Articles.Select(x=>new ArticleViewModel
           {
               ID = x.Id,
               Category = x.Category.Name,
               Picture = x.Picture,
               PublishDate = x.PublishDate.ToFarsi(),
               ShortDescription = x.ShortDescription,
               Title = x.Title

           });
           if (!string.IsNullOrWhiteSpace(searchModel.Title))
               query = query.Where(x => x.Title.Contains(searchModel.Title));
           if (searchModel.CategoryId > 0)
               query = query.Where(x => x.CategoryId == searchModel.CategoryId);

           return query.OrderByDescending(x => x.ID).ToList();
        }
    }
}
