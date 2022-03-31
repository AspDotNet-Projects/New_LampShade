using System;

namespace InventoryManagement.Domain.InventoryAgg
{
    /// <summary>
    /// ثبت لاگ انبارداری
    /// از ثبت ورود و خروج کالا تا شماره سقارش و نام انبار و نوع عملیات و ...
    /// </summary>
    public class InventoryOperation
    {
        public long Id { get; private set; }
        /// <summary>
        /// صفر برای موقعی که خروج
        /// و یک برای موقعی که ورود
        /// </summary>
        public bool Operation { get; private set; }
        /// <summary>
        /// چه تعداد وارد یا خارج شده
        /// </summary>
        public long Count { get; private set; }
        /// <summary>
        /// نوع عملیات اینترنتی بوده یا کارمند ما انجام داده
        /// </summary>
        public long OperationId { get; private set; }

        public DateTime OperationDate { get; private set; }
        /// <summary>
        /// با این فعل و انفعال مقدار انبار چقدر شده
        /// </summary>
        public long CurrentCount { get; private set; }
        public string Description { get; private set; }
        public long OrderId  { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get;private set; }
        static InventoryOperation() { }

        public InventoryOperation(bool operation, long count, long operationId,
            long currentCount, string description, long orderId, long inventoryId)
        {
            Operation = operation;
            Count = count;
            OperationId = operationId;
            CurrentCount = currentCount;
            Description = description;
            OrderId = orderId;
            InventoryId = inventoryId;
            OperationDate=DateTime.Now;
                
        }
    }
}