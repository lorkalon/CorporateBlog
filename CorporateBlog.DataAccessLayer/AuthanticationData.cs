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
		[Key]
		public string Login { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }

	    public virtual UserPersonalData UserPersonalData { get; set; }
		public virtual List<Category> Categories { get; set; }
    }
}
