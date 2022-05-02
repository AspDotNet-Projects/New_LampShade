namespace CommentManagement.Application.Contract.ProductComment
{
    public class AddProductComment
    {
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Message { get;  set; }
        public long ProductId { get;  set; }
    }
}
