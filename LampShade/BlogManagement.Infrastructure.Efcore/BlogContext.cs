﻿using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.Efcore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.Efcore
{
    public class BlogContext : DbContext
    {
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public BlogContext(DbContextOptions<BlogContext> options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ArticleCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}