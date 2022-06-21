using System.Collections.Generic;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
       

        public long Id { get; set; }
        public long RoleId{ get; set; }
        public string Role { get; set; }
        public string UserName{ get; set; }
        public string FullName{ get; set; }
        public string Mobile { get; set; }
        public string ProfilePhoto { get; set; }
        public List<int> Permissions{ get; set; }

        public AuthViewModel()
        {

        }


        public AuthViewModel(long id, long roleId, string userName, string fullName, string mobile,
            string profilephoto,List<int> permissions)
        {
            Id = id;
            RoleId = roleId;
            UserName = userName;
            FullName = fullName;
            Mobile = mobile;
            if(!string.IsNullOrWhiteSpace(profilephoto))
            ProfilePhoto = profilephoto;
            Permissions = permissions;
        }
    }
}