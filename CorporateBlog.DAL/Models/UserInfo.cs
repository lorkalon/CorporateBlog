using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorporateBlog.DAL.Models
{
    public class UserInfo:BaseEntity
    {
        [NotMapped]
        public new int Id { get; set; }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Avatar { get; set; }
        public virtual User User { get; set; }
    }
}
