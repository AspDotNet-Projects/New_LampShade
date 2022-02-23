﻿using System.Collections.Generic;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Application.Contract.Product
{
    public class CreateProduct
    {
        public string Name { get;  set; }
        public string Code { get;  set; }
        public double UnitPrice { get; set; }
        public string ShortDescription { get;  set; }
        public string Description { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public long CategoryId { get;  set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        //جهت مفداری دهی لیست بازشو در فرم ایجاد محصول
        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}
