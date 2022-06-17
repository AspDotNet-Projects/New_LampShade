using System.Collections.Generic;
using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : EntityBase
    {
        public string Name { get;private set; }
        public List<Permissions> Permissions { get; private set; }
        public List<Account> Accounts { get; private set; }

        protected Role()
        {

        }
        public Role(string name,List<Permissions> permissions)
        {
            Name = name;
            Permissions = permissions;
            Accounts = new List<Account>();
        }

        public void Edit(string name, List<Permissions> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}
