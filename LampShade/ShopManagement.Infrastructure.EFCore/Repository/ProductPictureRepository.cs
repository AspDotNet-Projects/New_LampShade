using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Repository;
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
                Picture = x.Picture,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var Query = _context.ProductPictures.Include(x => x.Product)
                .Select(x => new ProductPictureViewModel()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Picture = x.Picture,
                    CreationDate = x.CreationDate.ToString(),
                    Product = x.Product.Name
                });
            if (Query != null)
                Query = Query.Where(x => x.ProductId == searchModel.PorductId);

            return Query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
