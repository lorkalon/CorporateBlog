﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public virtual Common.User User { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int ArticleId { get; set; }
        public virtual Common.Article Article { get; set; }
        public int Rate { get; set; }

    }
}
