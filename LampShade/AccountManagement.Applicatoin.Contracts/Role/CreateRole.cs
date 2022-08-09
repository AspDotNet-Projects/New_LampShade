using _0_Framework.Application;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Applicatoin.Contracts.Role
{
    public class CreateRole
    {
        [Required(ErrorMessage = ValidationMesseges.IsRequired)]
        public string Name { get; set; }
        public List<int> permissions { get; set; }
       
    }
}
