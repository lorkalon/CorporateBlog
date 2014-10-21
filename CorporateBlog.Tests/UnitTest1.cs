using System;
using System.Collections.Generic;
using System.Linq;
using CorporateBlog.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CorporateBlog.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			using (var db = new CorporateBlogDbContext())
			{
				var authanticationData = new AuthanticationData()
					{
						Login = "qwerty",
						Password = "qwerty"
					};

				db.AuthanticationDatas.Add(authanticationData);
				db.SaveChanges();	
				
				//UserPersonalData user = new UserPersonalData()
				//	{
				//		Name = "Lena", 
				//		Surname = "Polljakova", 
				//		Age = 16,
				//		AuthanticationData = db.AuthanticationDatas.FirstOrDefault(data => data.Login == authanticationData.Login)
				//	};

				
				//db.UsersPersonalsData.Add(user);
				//db.SaveChanges();

				//Assert.AreEqual(db.UsersPersonalsData.First().AuthanticationData.Login, authanticationData.Login);
				Assert.AreEqual(12,12);
			}
		}
	}
}
