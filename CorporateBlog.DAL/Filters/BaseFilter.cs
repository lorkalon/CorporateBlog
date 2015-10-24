using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Filters
{
    public class BaseFilter
    {
        public int From { get; set; }
        public int Count { get; set; }
        public bool IsAscending { get; set; }
        public string SearchContent { get; set; }
    }
}
