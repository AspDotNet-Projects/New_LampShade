using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Configuration.Permissions
{
    public class DiscountPermissions
    {
        //coustomerDiscount
        public const int listCustomerDiscount = 100;
        public const int DefineCustomerDiscount = 101;
        public const int EditCustomerDiscount = 102;
        public const int SearchCustomerDiscount = 103;
        public const int ActiveCustomerDiscount = 104;


        //ColleagueDiscount
        public const int listColleagueDiscount = 110;
        public const int DefineColleagueDiscount = 111;
        public const int EditColleagueDiscount = 112;
        public const int SearchColleagueDiscount = 113;
        public const int ActiveColleagueDiscount = 114;


    }
}
