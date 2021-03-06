﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public virtual Common.User User { get; set; }
        public virtual Common.Category Category { get; set; }
        public int Rate { get; set; }

    }
}
