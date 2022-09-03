namespace ShopManagement.Application.Contract.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        double GetAmountBy(long id);
        string PaymentSuccedded(long orderId,long refId);

    }
}
