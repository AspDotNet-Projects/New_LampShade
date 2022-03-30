using System.Collections.Generic;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contract.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);

        /// <summary>
        /// چون امکان داره کاربر لیستی از کالا ها رو برای خرید انتخاب کنه
        /// که ما می خواهیم یکجا کم کنیم و لیست آنها را می گیریم
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        OperationResult Reduce(List<ReduceInventory> command);

        /// <summary>
        /// زمانی که کاربر می خواد یک کالا رو کاهش بده
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        OperationResult Reduce(ReduceInventory command);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">شماره اینونتوری آی دی است</param>
        /// <returns></returns>
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
    }
}
