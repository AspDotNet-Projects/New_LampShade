using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication:IArticleApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle commnad)
        {
            var operation = new OperationResult();
            if (_articleRepository.Exists(x=>x.Title==commnad.Title))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);

            var slug = commnad.Slug.Slugify();
            var categorySlug = _articleCategoryRepository.GetSlugById(commnad.CategoryId);
            var path = $"{categorySlug}//{slug}";

            var picturName = _fileUploader.Upload(commnad.Picture,path);
            var publishdate = commnad.PublishDate.ToGeorgianDateTime();

            var article = new Article(commnad.Title,commnad.ShortDescription,commnad.Description
            ,picturName,commnad.PictureAlt,commnad.PictureTitle, publishdate, slug,commnad.Keywords,
            commnad.MetaDescription, commnad.CanonicalAddress,commnad.CategoryId);
            
            _articleRepository.Create(article);
            _articleRepository.SaveChange();

            return operation.Succedded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var article = _articleRepository.GetWithCatgegory(command.Id);
            if (article == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);

            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id!=command.Id))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);

            var slug = command.Slug.Slugify();
            var path = $"{article.Category.Slug}//{slug}";

            var pictureName = _fileUploader.Upload(command.Picture, path); 
            var publishdate = command.PublishDate.ToGeorgianDateTime();
            article.Edit(command.Title, command.ShortDescription, command.Description
                , pictureName, command.PictureAlt, command.PictureTitle, publishdate, slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress, command.CategoryId);

           _articleRepository.SaveChange();
           return operation.Succedded();
        }

        public EditArticle Getdetails(long id)
        {
            return _articleRepository.Getdetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
