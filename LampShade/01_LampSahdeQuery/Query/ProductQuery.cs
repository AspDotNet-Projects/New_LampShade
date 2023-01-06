using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampSahdeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.Comment;
using _01_LampShadeQuery.Contracts.Product;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampShadeQuery.Query
{

    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventorContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentcontext;

        public ProductQuery(ShopContext shopContext, 
            InventoryContext inventorContext, 
            DiscountContext discountContext, 
            CommentContext commentcontext)
        {
            _shopContext = shopContext;
            _inventorContext = inventorContext;
            _discountContext = discountContext;
            _commentcontext = commentcontext;
        }

        public ProductQueryModel GetProductDetails(string slug)
        {
            var inventory = _inventorContext.Inventory.Select(x => new { x.ProductId, x.UnitePrice, x.InStock }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var product = _shopContext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    Code = x.Code,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    ShortDescription = x.ShortDescription,
                    Pictures = MapProductPinctures(x.ProductPictures)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (product == null)
                return new ProductQueryModel();

            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                product.IsInStock = productInventory.InStock;
                var price = productInventory.UnitePrice;
                product.Price = price.ToMoney();
                product.DoublePrice = price;
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }

            product.Comments = _commentcontext.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentType.Product)
                .Where(x => x.OwnerRecordId == product.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    CreationDate = x.CreationDate.ToFarsi()
                }).OrderByDescending(x => x.Id).ToList();

            return product;
        }

        //این متد حتما باید استاتیک باشد
        private static List<ProductPictureQueryModel> MapProductPinctures(List<ProductPicture> pictures)
        {
            return pictures.Select(x => new ProductPictureQueryModel
            {
                IsRemoved = x.IsRemoved,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).Where(x => x.IsRemoved == false).ToList();
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventorContext.Inventory.Select(x => new
                { x.ProductId, x.UnitePrice, }).ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();
            var products = _shopContext.Products.Include(x => x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Category = product.Category.Name,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug
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

        public List<CartItem> CheckInventoryStatus(List<CartItem> cartitems)
        {
            var inventory = _inventorContext.Inventory.ToList();
            foreach (var cartitem in cartitems)
            {
                if (inventory.Any(x => x.ProductId == cartitem.Id && x.InStock))
                {
                    var itemInventory = inventory.Find(x => x.ProductId == cartitem.Id);
                    if(itemInventory != null)
                        //age mojodi > darkhast bood
                    cartitem.IsInStock = itemInventory.CalculateCurrentCount()>=cartitem.Count;
                }
            }

            return cartitems;
        }

        public List<ProductQueryModel> Productcount()
        {
            var products = _inventorContext.Inventory
                .Select(prodct => new ProductQueryModel
                {
                    Id = prodct.ProductId,
                    Count = (int) prodct.CalculateCurrentCount()
                });

            var inventory = products.OrderByDescending(x => x.Id).ToList();
            inventory.ForEach(item =>
            {
                //? یعنی اگه مفدار نداشت مفدار خالی برگردونه که خطا نده
                item.Name = _shopContext.Products.FirstOrDefault(x => x.Id == item.Id)?.Name;
            });

            return inventory;
        }


    }
}
