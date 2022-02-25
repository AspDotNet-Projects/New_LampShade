using _0_Framework.Domain;
using ShopManagement.Application.Contract.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long,ProductPicture>
    {
        
        EditProductPicture GetDatails(long id);
        ProductPictureSearchModel Sarch(ProductPictureSearchModel searchModel);
    }
}
