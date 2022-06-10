﻿using _0_Framework.Domain;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Mobile{ get; private set; }
        public long  RoleId { get; private set; }
        public string ProfilePhoto  { get; private set; }

        public Account(string fullname, string username, string password, string mobile, long roleId, string profilePhoto)
        {
            Fullname = fullname;
            Username = username;
            Password = password;
            Mobile = mobile;
            RoleId = roleId;
            ProfilePhoto = profilePhoto;
        }
        /// <summary>
        /// پسورد اینجا عوض نمیشه 
        /// </summary>
        /// <param name="fullname"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="mobile"></param>
        /// <param name="roleId"></param>
        /// <param name="profilePhoto"></param>
        public void Edit(string fullname, string username,  string mobile, long roleId, string profilePhoto)
        {
            Fullname = fullname;
            Username = username;
            Mobile = mobile;
            RoleId = roleId;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}