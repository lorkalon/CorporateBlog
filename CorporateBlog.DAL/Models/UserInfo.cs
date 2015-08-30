namespace CorporateBlog.DAL.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Avatar { get; set; }

        public virtual User User { get; set; }
    }
}
