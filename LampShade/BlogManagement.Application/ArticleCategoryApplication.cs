using System.Collections.Generic;
using System.Security.Authentication;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategoryRepository.Exists(x=>x.Name==command.Name))
                return  operation.Failed(ApplicationMesseges.DuplicatedRecored);
            
            var slug = command.Slug.Slugify();
            var pictureName = _fileUploader.Upload(command.Picture, slug);

            var articlecategory=new ArticleCategory(command.Name,pictureName, 
                command.PictureAlt,command.PictureTitle,command.Description, command.ShowOrder, slug, 
                command.Keywords, command.MetaDescription,
                command.CanonicalAddress);
            _articleCategoryRepository.Create(articlecategory);
            _articleCategoryRepository.SaveChanges();

            return operation.Succedded();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var ariclecategory = _articleCategoryRepository.Get(command.Id);
            
            if (ariclecategory == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);

            if (_articleCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);

            var slug = command.Slug.Slugify();
            var pictureName = _fileUploader.Upload(command.Picture, slug);

            ariclecategory.Edit(command.Name, pictureName,command.PictureAlt,command.PictureTitle,
                command.Description, command.ShowOrder, slug,
                command.Keywords, command.MetaDescription,
                command.CanonicalAddress);
            _articleCategoryRepository.SaveChanges();

            return operation.Succedded();
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetAarticleCategories();
        }


        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}
