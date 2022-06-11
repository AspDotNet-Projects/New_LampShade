using AccountManagement.Applicatoin.Contracts.Role;
using System.Collections.Generic;
using _0_Framework.Domain;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository: IRepository<long,Role>
    {
        EditRole GetDetails(long id);
        List<RoleViewModel> List();
    }
}
