using System.Collections.Generic;

namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void Signin(AuthViewModel account);
        bool IsAuthenticated();
        void Signout();
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        List<int> GetPermission();
    }
}
