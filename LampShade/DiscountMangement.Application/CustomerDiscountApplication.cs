using System.Collections.Generic;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountMangement.Domain.CustomerDiscountAgg;

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
            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId
                                                        && x.DiscountRate == command.DiscountRate))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);
            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            var customerdiscount = new CustomerDiscount(command.ProductId, command.DiscountRate, startDate,
                endDate, command.Reason);
            _customerDiscountRepository.Create(customerdiscount);
            _customerDiscountRepository.SaveChange();
            return operation.Succedded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            var customerdiscount = _customerDiscountRepository.Get(command.Id);

            if (customerdiscount == null)
                operation.Failed(ApplicationMesseges.RecoredNotFound);

            if (_customerDiscountRepository.Exists(x => x.ProductId == command.ProductId
            && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
                operation.Failed(ApplicationMesseges.DuplicatedRecored);

            var startDate = command.StartDate.ToGeorgianDateTime();
            var endDate = command.EndDate.ToGeorgianDateTime();

            customerdiscount.Edit(command.ProductId,command.DiscountRate,startDate,endDate,command.Reason);
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
