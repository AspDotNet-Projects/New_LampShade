using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountMangement.Domain.CustomerDiscountAgg;
using Microsoft.VisualBasic;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepository.Exists(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                operation.Failed(ApplicationMesseges.DuplicatedRecored);
            var startdate = command.StartDate.ToGeorgianDateTime();
            var enddate = command.EndDate.ToGeorgianDateTime();
            var customerDiscount = new CustomerDiscount(command.ProductId, command.DiscountRate,
                startdate, enddate, command.Reason);
            _customerDiscountRepository.Create(customerDiscount);
            _customerDiscountRepository.SaveChange();
            return operation.Succedded();


        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation= new OperationResult();
            var customerdiscount = _customerDiscountRepository.Get(command.Id);
            if (customerdiscount == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);
            if (_customerDiscountRepository.Exists(x =>
                x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate
                                                 && x.Id != command.Id))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);
            
            var startdate = command.StartDate.ToGeorgianDateTime();
            var enddate = command.EndDate.ToGeorgianDateTime();
            customerdiscount.Edit(command.ProductId,command.DiscountRate,startdate,enddate,command.Reason);
            _customerDiscountRepository.SaveChange();
            return operation.Succedded();

        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id); 
        }

        public List<CustomerDiscountViewModel> Search(CostomerDiscountSearchModel search)
        {
            return _customerDiscountRepository.Search(search);
        }
    }
}
