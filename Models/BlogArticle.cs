namespace QuokkaLabAPI.Models
{
    public class BlogArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
        public string AuthorId { get; set; }
    }
}
