﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository :RepositoryBase<long,ProductPicture>,IProductPictureRepository
    {
        private readonly ShopContext _context;
        
        public ProductPictureRepository( ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDatails(long id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt

            }).FirstOrDefault(x => x.Id == id);
        }

        public ProductPicture GetWithProductAngCategory(long id)
        {
            //جویت بین سه جدول
            return _context.ProductPictures.Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == id);

        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures
                .Include(x => x.Product)
                .Select(x => new ProductPictureViewModel
                {
                    Id = x.Id,
                    Product = x.Product.Name,
                    CreationDate = x.CreationDate.ToFarsi(),
                    Picture = x.Picture,
                    ProductId = x.ProductId,
                    IsRemoved = x.IsRemoved
                });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
