using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampSahdeQuery.Contracts.Product;
using _01_LampSahdeQuery.Contracts.ProductCategory;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampSahdeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventorContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventorContext)
        {
            _shopContext = shopContext;
            _inventorContext = inventorContext;
        }

        public List<ProductCategoryQueryModel> getProductCategories()
        {
            return _shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventorContext.Inventory.Select(x => new
            {
                x.ProductId,
                x.UnitePrice,

            }).ToList();
            var  categoris=_shopContext.ProductCategories.Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).ToList();

            foreach (var category in categoris)
            {
                foreach (var product in category.Products)
                {
                    product.Price = inventory.FirstOrDefault(x => x.ProductId == product.Id)
                        ?.UnitePrice.ToMoney();
                }
                
            }
            return categoris;

        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(Product => new ProductQueryModel
            {
                Id = Product.Id,
                Name = Product.Name,
                Category = Product.Category.Name,
                Picture = Product.Picture,
                PictureAlt = Product.PictureAlt,
                PictureTitle = Product.PictureTitle
            }).ToList();



        }
    }
}
