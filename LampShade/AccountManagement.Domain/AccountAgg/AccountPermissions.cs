namespace AccountManagement.Domain.AccountAgg
{
    public class AccountPermissions
    {
        public long ID { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }
        public long AccountId { get; private set; }
        public Account Account { get; private set; }

       

        public AccountPermissions(int code, string name)
        {
            Code = code;
            Name = name;
        }

        public AccountPermissions(int code)
        {
            Code = code;
        }
    }
}
