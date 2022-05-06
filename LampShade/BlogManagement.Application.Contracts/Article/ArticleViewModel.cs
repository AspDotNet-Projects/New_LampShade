namespace BlogManagement.Application.Contracts.Article
{
    public class ArticleViewModel
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Picture { get; set; }
        public string PublishDate { get; set; }
        public string Category { get; set; }
        public long CategoryId { get; set; }

    }
}