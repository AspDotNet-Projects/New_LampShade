using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampSahdeQuery.Contracts.Product;
using _01_LampSahdeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
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
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventorContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventorContext = inventorContext;
            _discountContext = discountContext;
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
        {
            var inventory = _inventorContext.Inventory.Select(x => new
            { x.ProductId, x.UnitePrice, }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId ,x.EndDate}).ToList();
            var categoriy = _shopContext.ProductCategories.Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products),
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    Slug = x.Slug
                }).FirstOrDefault(x=>x.Slug==slug);

            
                foreach (var product in categoriy.Products)
                {
                    var productinventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);

                    if (productinventory != null)
                    {
                        var price = productinventory.UnitePrice;
                        product.Price = price.ToMoney();

                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount != null)
                        {
                            int discountRate = discount.DiscountRate;
                            product.DiscountRate = discountRate;
                            //برای نمایش خط روی مبلغ اگر تخفیف داشت
                            product.HasDiscouont = discountRate > 0;
                            //مقدار تخفیف
                            var discountAmount = Math.Round((price * discountRate) / 100);

                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                            product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                        }
                    }



                }
           
            return categoriy;
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
            { x.ProductId, x.UnitePrice,}).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x=>x.StartDate<DateTime.Now && x.EndDate>DateTime.Now)
                .Select(x => new {x.DiscountRate, x.ProductId}).ToList();
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
                    var productinventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    
                    if (productinventory != null)
                    {
                        var price = productinventory.UnitePrice;
                        product.Price = price.ToMoney();

                        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount != null)
                        {
                            int discountRate = discount.DiscountRate;
                            product.DiscountRate = discountRate;
                            //برای نمایش خط روی مبلغ اگر تخفیف داشت
                            product.HasDiscouont = discountRate > 0;
                            //مقدار تخفیف
                            var discountAmount = Math.Round((price * discountRate) / 100);

                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }
                   
                   

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
                PictureTitle = Product.PictureTitle,
              

            }).ToList();



        }
    }
}
