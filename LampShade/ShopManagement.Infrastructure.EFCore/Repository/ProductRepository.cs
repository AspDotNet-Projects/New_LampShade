﻿using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository:RepositoryBase<long,Product>,IProductRepository
    { private readonly ShopContext _context;
        public ProductRepository( ShopContext context) : base(context)
        {
            _context = context;
        }

        public Product GetProductWithCategory(long id)
        {
            return _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public EditProduct GetDetails(long id)
        {
            return _context.Products.Select(x => new EditProduct()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Slug = x.Slug,
                CategoryId = x.CategoryId,
                
                
                

            }).FirstOrDefault(x=>x.Id==id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x=>new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public List<ProductViewModel> search(ProductSearchModel searchModel)
        {
            var query = _context.Products.Include(x=>x.Category )
                .Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                CategoryId=x.CategoryId,
                Code = x.Code,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi()
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query=query.Where(x => x.Name.Contains(searchModel.Name));
            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));
            if (searchModel.CategoryId!=0)
                query = query.Where(x => x.CategoryId==searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();


        }
    }
}
