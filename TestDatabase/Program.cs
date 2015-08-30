using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL;
using CorporateBlog.DAL.DbContextProvider;

namespace TestDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new CorporateBlogContext())
            {
                var rows = db.Articles.ToList();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey(); 
            }
        }
    }
}
