﻿using CommentManagement.Application;
using CommentManagement.Application.Contract.ProductComment;
using CommentManagement.Domain.ProductCommentAgg;
using CommentManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommentManagement.Configuration
{
    public class CommentManagementBootstrapper
    {
        public static void Configur(IServiceCollection services, string connectionstring)
        {
            services.AddTransient<IProductCommentRepository, ProductCommentRepository>();
            services.AddTransient<IProductCommentApplication, ProductCommentApplication>();

            services.AddDbContext<CommentContext>(x => x.UseSqlServer(connectionstring));
        }
    }
}
