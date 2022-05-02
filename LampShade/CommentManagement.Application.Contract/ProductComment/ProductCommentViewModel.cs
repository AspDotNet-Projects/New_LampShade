namespace CommentManagement.Application.Contract.ProductComment
{
    public class ProductCommentViewModel
    {
        public long Id{ get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public long ProductId { get; set; }
        public bool IsConfirm { get;  set; }
        public bool IsCanceled { get;  set; }
        public string ProductName { get; set; }
        public string CommentDate { get; set; }

    }
}