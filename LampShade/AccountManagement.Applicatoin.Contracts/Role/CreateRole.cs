using System.Collections.Generic;

namespace AccountManagement.Applicatoin.Contracts.Role
{
    public class CreateRole
    {
        public string Name { get; set; }
        public List<int> permissions { get; set; }
       
    }
}
