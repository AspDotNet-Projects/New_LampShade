using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore
{
    public class PaymentMethod
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        private PaymentMethod(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public  static List<PaymentMethod> GetList()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod(1, "پرداخت اینترنتی",
                    "در این مدل شما به درگاه اینترنتی هدایت شده و در لحظه پرداخت وجه را انجام خواهید داد."),
                new PaymentMethod(2, "پرداخت نقدی",
                    "در این مدل ابتدا سفارش ثبت شده وجه به صورت نقدی در زمان کالا دریافت خواهد شد.")

            };
        }

        public static PaymentMethod GetBy(long id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }
    }
}
