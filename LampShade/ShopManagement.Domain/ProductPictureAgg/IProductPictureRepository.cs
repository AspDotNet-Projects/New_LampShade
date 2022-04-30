using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.Contract.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long,ProductPicture>
    {
        
        EditProductPicture GetDatails(long id);
        ProductPicture GetWithProductAngCategory(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
