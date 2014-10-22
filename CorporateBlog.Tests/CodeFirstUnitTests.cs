using System;
using System.Collections.Generic;
using System.Linq;
using CorporateBlog.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CorporateBlog.Tests
{
	[TestClass]
	public class CodeFirstUnitTests
	{
		string login = "SomeLogin";

		[TestMethod]
		public void AddAuthenticationData()
		{
			using (var db = new CorporateBlogDbContext())
			{
				var existing = db.AuthanticationDatas.Where(data => data.Login == login).ToList();

				if (existing.Any())
				{
					throw new Exception(login + " auth data already exist!");
				}

				var authanticationData = new AuthanticationData()
					{
						Login = login,
						Email = "email@mail.com",
						Password = "qwerty"
					};

				db.AuthanticationDatas.Add(authanticationData);
				db.SaveChanges();

				Assert.IsNotNull(db.AuthanticationDatas.FirstOrDefault(data => data.Login == login));
			}
		}

		[TestMethod]
		public void AddUserPersonalInformation()
		{
			using (var db = new CorporateBlogDbContext())
			{
				var existing = db.AuthanticationDatas.Where(data => data.Login == login).ToList();

				if (!existing.Any())
				{
					throw new Exception(login + " auth data not found!");
				}

				var guid = Guid.NewGuid();

				var user = new UserPersonalData()
					{
						UserPersonalDataId = guid,
						Name = "Lena",
						Surname = "Polljakova",
						Age = 16,
						AuthanticationData = existing.First()
					};


				db.UsersPersonalsData.Add(user);
				db.SaveChanges();

				Assert.IsNotNull(db.UsersPersonalsData.FirstOrDefault(data => data.UserPersonalDataId == guid));
			}
		}

		[TestMethod]
		public void AddCategory()
		{
			using (var db = new CorporateBlogDbContext())
			{
				var guid = Guid.NewGuid();

				var category = new Category()
					{
						CategoryId = guid,
						Title = "New blog",
						Description = "Tralala",
						Author = db.AuthanticationDatas.FirstOrDefault(data => data.Login == login)
					};

				db.Categories.Add(category);
				db.SaveChanges();
				Assert.IsNotNull(db.Categories.FirstOrDefault(data => data.CategoryId == guid));
			}
		}

		[TestMethod]
		public void AddPost()
		{
			using (var db = new CorporateBlogDbContext())
			{
				var guid = Guid.NewGuid();
				var post = new Post()
					{
						PostId = guid,
						Title = "Post1",
						Content = "Content of post1",
						Category = db.Categories.FirstOrDefault(category => category.Author.Login == login)
					};

				db.Posts.Add(post);
				db.SaveChanges();
				Assert.IsNotNull(db.Posts.FirstOrDefault(t=>t.PostId == guid));
			}
		}


		[TestMethod]
		public void CheckAuthData()
		{
			using (var db = new CorporateBlogDbContext())
			{
				var authData = db.AuthanticationDatas.FirstOrDefault(data => data.Login == login);

				if (authData == null)
				{
					throw new Exception(login + " auth not found!");
				}

				Assert.IsNotNull(authData.UserPersonalData);
				Assert.AreEqual(authData.UserPersonalData.Surname, "Polljakova");
				Assert.IsNotNull(authData.Categories);
				Assert.IsTrue(authData.Categories.Count > 0);
				Assert.IsNotNull(authData.Categories.First().Posts);
				Assert.IsTrue(authData.Categories.First().Posts.Count > 0);
			}
		}
	}
}
