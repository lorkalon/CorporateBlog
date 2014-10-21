using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.DataAccessLayer
{
	public class UserPersonalData
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long UserPersonalDataId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public int Age { get; set; }

		public virtual AuthanticationData AuthanticationData { get; set; }

	}
}
