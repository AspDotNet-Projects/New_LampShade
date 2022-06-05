using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Article;
using _01_LampShadeQuery.Contracts.Comment;
using BlogManagement.Infrastructure.Efcore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampShadeQuery.Query
{
    public class ArticleQuery:IArticleQuery
    {
        // رو اینجکت کردیمBlogContext  
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
        }

        public List<ArticleQueryModel> LatestArticle()
        {
            return _blogContext.Articles
                .Include(x => x.Category)
                .Where(x=>x.PublishDate<=DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                   
                    CategoryId = x.CategoryId,

                    PublishDate = x.PublishDate.ToFarsi(),
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    ShortDescription = x.ShortDescription,
                    Title = x.Title
                }).ToList();
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article= _blogContext.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    CategodrySlug = x.Category.Slug,
                    PublishDate = x.PublishDate.ToFarsi(),
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CanonicalAddress = x.CanonicalAddress,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    ShortDescription = x.ShortDescription,
                    Title = x.Title
                }).FirstOrDefault(x=>x.Slug==slug);

            article.Comments = _commentContext.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentType.Article)
                .Where(x => x.OwnerRecordId == article.Id)
                .Select(x => new CommentQueryModel()
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();

                ///در واقع جدا کننده است
            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordsList = article.Keywords.Split(",").ToList();
            return article;
        }
    }
}
