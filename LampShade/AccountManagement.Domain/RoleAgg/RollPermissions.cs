using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class RollPermissions
    {
        public long ID { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }


        public RollPermissions( int code, string name)
        {
           
            Code = code;
            Name = name;
        }

        public RollPermissions(int code)
        {
            Code = code;
        }
    }
}