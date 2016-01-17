using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorporateBlog.Common;

namespace CorporateBlog.WebApi.Models
{
    public class CommentRate
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public RateType Value { get; set; }
    }
}