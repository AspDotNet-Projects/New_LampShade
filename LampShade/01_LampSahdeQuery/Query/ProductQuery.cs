using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampSahdeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{

    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventorContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventorContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventorContext = inventorContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventorContext.Inventory.Select(x => new
                { x.ProductId, x.UnitePrice, }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();
            var products = _shopContext.Products.Include(x => x.Category)
                .Select(Product => new ProductQueryModel
                {
                    Id = Product.Id,
                    Name = Product.Name,
                    Category = Product.Category.Name,
                    Picture = Product.Picture,
                    PictureAlt = Product.PictureAlt,
                    PictureTitle = Product.PictureTitle,
                    //Take(6) ==>6 charachter end
                }).AsNoTracking().OrderByDescending(x=>x.Id).Take(6).ToList();
            foreach (var product in products)
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
                        product.HasDiscount = discountRate > 0;
                        //مقدار تخفیف
                        var discountAmount = Math.Round((price * discountRate) / 100);

                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }



            }

            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventorContext.Inventory.Select(x =>
                new { x.ProductId, x.UnitePrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var query = _shopContext.Products
                .Include(x => x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Category = product.Category.Name,
                    CategorySlug = product.Category.Slug,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    ShortDescription = product.ShortDescription,
                    Slug = product.Slug
                }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));
            var products = query.OrderByDescending(x => x.Id).ToList();
                foreach (var product in products)
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
                            product.HasDiscount = discountRate > 0;
                            //مقدار تخفیف
                            var discountAmount = Math.Round((price * discountRate) / 100);

                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                        }
                    }



               
            }
            return products;
        }

    }
}
