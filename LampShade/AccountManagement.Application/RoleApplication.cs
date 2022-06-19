using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Applicatoin.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication:IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
           var  operation = new OperationResult();
           if (_roleRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);

            var role = new Role(command.Name,new List<Permissions>());
            _roleRepository.Create(role);
            _roleRepository.SaveChange();

            return operation.Succedded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var role = _roleRepository.Get(command.Id);
            if (role == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);
            if (_roleRepository.Exists(x => x.Name == command.Name && x.Id!=command.Id))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);

            var permissions = new List<Permissions>();
            command.permissions.ForEach(code=>permissions.Add(new Permissions(code.ToString())));

            role.Edit(command.Name,permissions);
            _roleRepository.SaveChange();

            return operation.Succedded();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }

        public List<RoleViewModel> List()
        {
            return _roleRepository.List();
        }
    }
}
