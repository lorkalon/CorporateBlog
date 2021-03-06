﻿namespace CorporateBlog.DAL.Models
{
    public class CommentRate:BaseEntity
    {
        public int Value { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

    }
}
