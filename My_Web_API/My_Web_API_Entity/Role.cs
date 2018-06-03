using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Web_API_Entity
{
	[Table("Role")]
	public class Role : AggregateRoot
	{
		public string Name {
			get;
			set;
		}

		public bool IsAdmin {
			get;
			set;
		}

		public string Remark {
			get;
			set;
		}

		public string CreatorName {
			get;
			set;
		}

		public DateTime? CreateTime {
			get;
			set;
		}
	}
}
