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

				Assert.AreNotEqual(db.AuthanticationDatas.FirstOrDefault(data => data.Login == login), null);
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

				Assert.AreNotEqual(db.UsersPersonalsData.FirstOrDefault(data => data.UserPersonalDataId == guid), null);
			}
		}

		[TestMethod]
		public void AddBlog()
		{
			using (var db = new CorporateBlogDbContext())
			{
				var guid = Guid.NewGuid();

				var blog = new Blog()
					{
						BlogId = guid,
						Title = "New blog",
						Description = "Tralala",
						Blogger = db.AuthanticationDatas.FirstOrDefault(data => data.Login == login)
					};

				db.Blogs.Add(blog);
				db.SaveChanges();
				Assert.AreNotEqual(db.Blogs.FirstOrDefault(data => data.BlogId == guid), null);
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
				Assert.IsNotNull(authData.Blogs);
				Assert.IsTrue(authData.Blogs.Count > 0);
			}
		}
	}
}
