﻿using System.Collections.Generic;
using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Applicataion
{
    public class InventoryApplication:IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);
            var inventory = new Inventory(command.ProductId, command.UnitePrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);

            if (_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);
            inventory.Edit(command.ProductId,command.UnitePrice);
            _inventoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);

            const long operatorid = 1;
            inventory.Increese(command.Count,operatorid,command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            //ذخبره به صورت لیستی
            var operation = new OperationResult();
            const long operatorid = 1;

            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);
                inventory.Reduce(item.Count,operatorid,item.Description,item.OrderId);
            }
            _inventoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);

            const long operatorid = 1;
            //orderid=0
            //چون انبار دار داره انحام میده
            inventory.Reduce(command.Count,operatorid,command.Description,0);
            _inventoryRepository.SaveChanges();
            return operation.Succedded();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryid)
        {
            return _inventoryRepository.GetOperationLog(inventoryid);
        }

    }
}
