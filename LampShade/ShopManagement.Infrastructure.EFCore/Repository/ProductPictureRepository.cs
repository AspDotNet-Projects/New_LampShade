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
        
        public ProductPictureRepository(DbContext context) : base(context)
        {
        }

        public EditProductPicture GetDatails(long id)
        {
            throw new NotImplementedException();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
