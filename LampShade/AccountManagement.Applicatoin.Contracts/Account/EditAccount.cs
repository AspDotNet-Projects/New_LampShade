using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace AccountManagement.Applicatoin.Contracts.Account
{
    public class EditAccount : RegisterAccount
    {
        public long Id { get; set; }
        public List<PermissionDto> Mappedpermissions { get; set; }
    }
}