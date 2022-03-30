using System.Collections.Generic;
using System.Linq;
using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitePrice { get; private set; }
        public bool InStock { get; private set; }
        /// <summary>
        ///  لیستی از قعل و انفعالات انبار را دارد
        /// </summary>
        public List<InventoryOperation> Operations { get;private set; }

        public Inventory(long productId, double unitePrice)
        {
            ProductId = productId;
            UnitePrice = unitePrice;
            ///در هنگام تعریف ما فالس می کنیم چون در هنگام تعریف هیچ فعل و انفعالی
            /// وجود ندارد و در واقع مثل این که در انبار فقی یک قفسه تعریف کرده باشیم
            InStock = false;
        }


        /// <summary>
        /// محاسبه مقدار فعلی با محاسبه سرجمع خروجی ها -سرجمع ورودی ها
        /// </summary>
        /// <returns></returns>
        private long CalculateCurrentCount()
        {
            ///x=>x.Operation Equals == 1
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            ///x=>!x.Operation Equals == 0
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        /// <summary>
        /// می خواهیم موجوری را افزایش دهیم که نباز داریم به پارامترهای ورودی ریر
        /// </summary>
        /// <param name="count">j\تعداد</param>/param>
        /// <param name="operationid">کسی که انجام داده</param>
        /// <param name="description">توضیحات</param>
        public void Increese(long count, long operatorid, string description)
        {
            /// CalculateCurrentCount() + count
            /// با کانت جمع شد تا مقدار که قرار هست جمع شود هم محاسبه شود
            var currentcount = CalculateCurrentCount() + count;
            ////پارامتر اول ترو چون می خواهیم افزایس انجام بدیم
            /// و مقدار صفر هم چون مشتری نمیتونه افزایش موجودی بده و خودمون اونو صفر می کنیم
            /// و آی دی هم آِ دی همین جدول
            var operation = new InventoryOperation(true, count, operatorid, currentcount
                , description, 0, Id);
            this.Operations.Add(operation);
            //اگر بزرگتر از صفر بود ترو شو و اگر کوچک تر بود فالس شود
            //یا
            //if (currentcount > 0)
            //    InStock = true;
            //else
            //    InStock = false;
         
            InStock = currentcount > 0;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="operatorid"></param>
        /// <param name="description"></param>
        /// <param name="orderid">داریم چون امکان داره مشتری خرید کنه یا خود انبار دار موجوری رو کم کنه</param>
        public void Reduce(long count, long operatorid, string description, long orderid)
        {
            var currentcount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(false, count, operatorid, currentcount,
                description, orderid, Id);
            Operations.Add(operation);
            InStock = currentcount >0;
        }

    }
}
