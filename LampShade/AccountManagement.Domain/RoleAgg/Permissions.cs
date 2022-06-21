namespace AccountManagement.Domain.RoleAgg
{
    public class Permissions
    {
        public long ID { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }

        public Permissions( int code, string name)
        {
           
            Code = code;
            Name = name;
        }

        public Permissions(int code)
        {
            Code = code;
        }
    }
}