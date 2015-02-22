namespace CorporateBlog.DAL.Models
{
    public class ArticleRate
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
