using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace AccountManagement.Applicatoin.Contracts.Role
{
    public class EditRole : CreateRole
    {
        public long Id { get; set; }
        public List<PermissionDto> Mappedpermissions { get; set; }
    }
}