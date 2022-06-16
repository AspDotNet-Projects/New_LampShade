﻿using System.Collections.Generic;
using _0_Framework.Application;
using AccountManagement.Applicatoin.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.AspNetCore.WebUtilities;

namespace AccountManagement.Application
{
    public class AccountApplication: IAccountApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccountRepository _acountRepositroy;
        private readonly IAuthHelper _authHelper;

        public AccountApplication(IAccountRepository acountRepositroy, IPasswordHasher passwordHasher, IFileUploader fileUploader, IAuthHelper authHelper)
        {
            _acountRepositroy = acountRepositroy;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
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

            if (_acountRepositroy.Exists(x => (x.Username == command.Username || x.Mobile == command.Mobile) && x.Id !=command.Id ))
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

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _acountRepositroy.GetBy(command.UserName);
            if (account == null)
                return operation.Failed(ApplicationMesseges.WrongUserPass);

            //_passwordHasher.Check(account.Password, command.Password);
            //مقایسه پسورد ذخیره شده با پسورد وارد شده
            //(bool verified,bool needsUpgrade) result
            //نید آپگرید مشخص میکنه که پسورد قوی هست یا نه و اکه بخواهیم بفهمیم که پسورد درسته یا غلط وریفای درست میشه یا غلط
            (bool verified,bool needsUpgrade) result=_passwordHasher.Check(account.Password, command.Password);
            if(!result.verified)
                return operation.Failed(ApplicationMesseges.WrongUserPass);

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Username,
                account.Fullname, account.Mobile);    
             
            _authHelper.Signin(authViewModel);
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

        public void Logout()
        {
            _authHelper.Signout();
        }
    }
}
