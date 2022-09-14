using System.Collections.Generic;

namespace ShopManagement.Application.Contract.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        double GetAmountBy(long id);
        void Cancel(long id);
        List<OrderItemViewModel> GetItems(long orderId);
        string PaymentSuccedded(long orderId,long refId);
        List<OrderViewModel> Search(OrderSearchModel searchModel);
    }
}
