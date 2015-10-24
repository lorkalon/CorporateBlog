namespace CorporateBlog.DAL.Models
{
    public class UserInfo:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Avatar { get; set; }

        public virtual User User { get; set; }
    }
}
