using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Applicatoin.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application
{
    public class AccountApplication: IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _acountRepositroy;

        public AccountApplication(IAccountRepository acountRepositroy, IPasswordHasher passwordHasher, IFileUploader fileUploader)
        {
            _acountRepositroy = acountRepositroy;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateAccount command)
        {
            var operation = new OperationResult();

            if (_acountRepositroy.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);
            
            var password = _passwordHasher.Hash(command.Password);
            
            var path = $"profilephotos";
            var picturepath = _fileUploader.Upload(command.ProfilePhoto, path);
            
            var account = new Account(command.Fullname, command.Username, password, command.Mobile, command.RoleId,picturepath);
            
            _acountRepositroy.Create(account);
            _acountRepositroy.SaveChange();
           
            return operation.Succedded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            var account = _acountRepositroy.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);

            if (_acountRepositroy.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile && x.Id==command.Id ))
                return operation.Failed(ApplicationMesseges.DuplicatedRecored);


            

            var path = $"profilephotos";
            var picturepath = _fileUploader.Upload(command.ProfilePhoto, path);

            account.Edit(command.Fullname, command.Username, command.Mobile, command.RoleId, picturepath);
            _acountRepositroy.SaveChange();

            return operation.Succedded();
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();
            var account = _acountRepositroy.Get(command.Id);

            if (command.Password == null)
                return operation.Failed(ApplicationMesseges.RecoredNotFound);
            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMesseges.PasswordNotMatch);
            
            //هش کردن پسورد
            var password = _passwordHasher.Hash(command.Password);
            
            account.ChangePassword(password);
            _acountRepositroy.SaveChange();

            return operation.Succedded();
            
        }

        public EditAccount GetDetails(long id)
        {
            return _acountRepositroy.GetDetails(id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _acountRepositroy.Search(searchModel);   
        }
    }
}
