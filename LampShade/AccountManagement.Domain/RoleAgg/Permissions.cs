namespace AccountManagement.Domain.RoleAgg
{
    public class Permissions
    {
        public long ID { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }

        public Permissions( string code, string name)
        {
           
            Code = code;
            Name = name;
        }

        public Permissions(string code)
        {
            Code = code;
        }
    }
}