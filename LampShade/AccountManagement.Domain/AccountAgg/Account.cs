using System.Collections.Generic;
using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }
        public string ProfilePhoto { get; private set; }
        public List<AccountPermissions> Permissions { get;private  set; }

        public Account()
        {
        }

        public Account(string fullname, string username, string password, string mobile,
            long roleId, string profilePhoto, List<AccountPermissions> permissions)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            Mobile = mobile;
            RoleId = roleId;

            if (roleId == 0)
                RoleId = 3;

            ProfilePhoto = profilePhoto;
            Permissions = permissions;
        }

        public void Edit(string fullname, string username, string mobile,
            long roleId, string profilePhoto, List<AccountPermissions> permissions)
        {
            Fullname = fullname;
            Username = username;
            Mobile = mobile;
            RoleId = roleId;

            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;
            Permissions = permissions;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}