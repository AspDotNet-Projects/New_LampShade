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

        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            throw new System.NotImplementedException();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            throw new System.NotImplementedException();
        }

        public List<CustomerDiscountViewModel> Search(CostomerDiscountSearchModel search)
        {
            throw new System.NotImplementedException();
        }
    }
}
