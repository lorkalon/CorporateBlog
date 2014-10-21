using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.DataAccessLayer
{
	public class AuthanticationData
    {
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long AuthanticateDataId { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

	    public virtual UserPersonalData UserPersonalData { get; set; }
    }
}
