using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace CommentManagement.Domain.ProductCommentAgg
{
    public class ProductComment : EntityBase
    {
        public string Name { get; private set; }
        public string  Email { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public long ProductId { get; private set; }
        public Product Product { get; set; }

        public ProductComment(string name, string email,
            string message, long productId)
        {
            Name = name;
            Email = email;
            Message = message;
            ProductId = productId;
        }

        public void Confirm()
        {
            IsConfirmed = true;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}
