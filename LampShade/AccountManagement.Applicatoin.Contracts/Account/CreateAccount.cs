﻿namespace AccountManagement.Applicatoin.Contracts.Account
{
    public class CreateAccount
    {
        public string Fullname { get;  set; }
        public string Username { get;  set; }
        public string Password { get;  set; }
        public string Mobile { get;  set; }
        public long RoleId { get;  set; }
        public string ProfilePhoto { get;  set; }
    }
}
