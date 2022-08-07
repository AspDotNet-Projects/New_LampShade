using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace BlogManagement.Configuration.Permissions
{
   public class BlogPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Article",new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.listArticles,"listArticles"),
                        new PermissionDto(BlogPermissions.CreateArticles,"CreateArticles"),
                        new PermissionDto(BlogPermissions.EditArticles,"EditArticles"),
                        new PermissionDto(BlogPermissions.SearchArticles,"SearchArticles"),

                    }
                },
                {
                    "ArticleCategory",new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.ListArticleCategory,"ListArticleCategory"),
                        new PermissionDto(BlogPermissions.CreateArticleCategory,"CreateArticleCategory"),
                        new PermissionDto(BlogPermissions.EditArticleCategory,"EditArticleCategory"),
                        new PermissionDto(BlogPermissions.SearchArticleCategory,"SearchArticleCategory"),

                    }
                }
            };
        }
    }
}
