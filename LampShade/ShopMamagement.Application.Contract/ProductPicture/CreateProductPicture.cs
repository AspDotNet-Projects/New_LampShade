﻿namespace ShopManagement.Application.Contract.ProductPicture
{
    public class CreateProductPicture
    {
        public long ProductId { get; set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public bool IsRemoved { get;  set; }
    }
}
