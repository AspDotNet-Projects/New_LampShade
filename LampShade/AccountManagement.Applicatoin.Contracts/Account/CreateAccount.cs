using System.Collections.Generic;
using AccountManagement.Applicatoin.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Applicatoin.Contracts.Account
{
    public class CreateAccount
    {
        public string Fullname { get;  set; }
        public string Username { get;  set; }
        public string Password { get;  set; }
        public string Mobile { get;  set; }
        public long RoleId { get;  set; }
        public List<RoleViewModel> Roles { get; set; }
        public IFormFile ProfilePhoto { get;  set; }
    }
}
