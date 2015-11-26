using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        public class Article
        {
            public int CategoryId { get; set; }
            public int Value { get; set; }
        }


        [TestMethod]
        public void TestMethod1()
        {
            var articles = getArticles(null, null).ToList();

            Assert.AreEqual(articles.Count(), 4);
        }


        [TestMethod]
        public void TestMethod2()
        {
            var articles = getArticles(2, 3).ToList();

            Assert.AreEqual(articles.Count(), 1);
            Assert.AreEqual(articles.First().Value, 3);

        }

        private IEnumerable<Article> getArticles(int? category, int? value)
        {
             var list = new List<Article>()
            {
                new Article()
                {
                    CategoryId = 1,
                    Value = 1

                },

                new Article()
                {
                    CategoryId = 2,
                    Value = 2
                },
                new Article()
                {
                    CategoryId = 2,
                    Value = 3
                },
                new Article()
                {
                    CategoryId = 3,
                    Value = 4
                }
            };

            return
                list.Where(
                    article => (category.HasValue && article.CategoryId == category.Value) || !(category.HasValue))
                    .Where(article => (value.HasValue && article.Value == value.Value) || !(value.HasValue));
        } 
    }
}
