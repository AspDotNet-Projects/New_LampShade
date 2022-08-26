namespace ShopManagement.Application.Contract.Order
{
    public interface ICartService
    {
        Cart Get();
        void Set(Cart cart);
    }
}
