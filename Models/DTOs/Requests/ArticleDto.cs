namespace QuokkaLabAPI.Models.DTOs.Requests
{
    public class ArticleDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
        public string AuthorId { get; set; }
    }
}
