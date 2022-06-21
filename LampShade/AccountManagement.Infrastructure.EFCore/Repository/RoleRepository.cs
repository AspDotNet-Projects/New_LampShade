using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Applicatoin.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository:RepositoryBase<long,Role>, IRoleRepository
    {
        private readonly AccountContext _accountContext;
        public RoleRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public EditRole GetDetails(long id)
        {
            var role= _accountContext.Roles.Select(x => new EditRole
            {
                Id = x.Id,
                Name = x.Name,
                Mappedpermissions = MapPermisstins(x.Permissions)
                }).AsNoTracking()
                .FirstOrDefault(x=>x.Id==id);
            
            //role.permissions = role.Mappedpermissions.Select(x => x.Code).ToList();
            return role;
        }

        private static List<PermissionDto> MapPermisstins(IEnumerable<Permissions> permissions)
        {
            return permissions.Select(x => new PermissionDto(int.Parse(x.Code),x.Name)).ToList();
        }

        public List<RoleViewModel> List()
        {
            return _accountContext.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();
        }
    }
}
