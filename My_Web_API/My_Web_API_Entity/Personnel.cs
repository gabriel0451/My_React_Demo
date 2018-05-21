using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Web_API_Entity
{
	[Table("Page")]
	public class Personnel : BaseEntity<int>
	{
		public string LoginId {
			get;
			set;
		}

		public string Name{
			get;
			set;
		}

		public string Password {
			get;
			set;
		}

		public string CellNumber {
			get;
			set;
		}

		public string Email {
			get;
			set;
		}

		public string Photo {
			get;
			set;
		}
	}
}
