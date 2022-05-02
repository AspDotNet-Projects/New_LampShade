using System.Collections.Generic;
using _0_Framework.Domain;


namespace CommentManagement.Domain.ProductCommentAgg
{
    public class Comment : EntityBase
    {
        public string Name { get; private set; }
        public string  Email { get; private set; }
        public string Website { get; private set; }

        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        //صاحب این کامنت
        public long  OwnerRecordId{ get; private set; }
        //بسته  به نوع موجودیت ما مقدار میگریه کامنت ممحصول است یا مقاله
        public int Type { get; private set; }
        
        /// <summary>
        /// این دو خصوصیت برای مواردی که به کامنت پاسخ داده می شود
        /// </summary>
        public long  ParentId { get; private set; }
        public Comment Parent { get; private set; }
        public List<Comment> Children { get; private set; }


        public  Comment() { }
        public Comment(string name, string email, string website, 
            string message, long ownerRecordId, int type, long parentId)
        {
            Name = name;
            Email = email;
            Website = website;
            Message = message;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
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
