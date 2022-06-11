using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using AccountManagement.Applicatoin.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Applicatoin.Contracts.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Fullname { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Username { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Password { get;  set; }

        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Mobile { get;  set; }

        [Range(1,int.MaxValue,ErrorMessage = ValidationMesseges.IsRequired)]
        public long RoleId { get;  set; }

        public List<RoleViewModel> Roles { get; set; }
        public IFormFile ProfilePhoto { get;  set; }
    }
}
