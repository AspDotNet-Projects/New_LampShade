namespace CommentManagement.Application.Contract.ProductComment
{
    public class CommentViewModel
    {
        public long Id{ get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Website { get; set; }
        public bool IsConfirm { get;  set; }
        public bool IsCanceled { get;  set; }
        public string OwnerName { get; set; }
        public long OwnerRecoredId { get; set; }
        public int Type { get; set; }

        public string CommentDate { get; set; }

    }
}