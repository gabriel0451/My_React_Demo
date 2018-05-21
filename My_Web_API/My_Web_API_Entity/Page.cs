using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Web_API_Entity
{
	[Table("Page")]
	public class Page : BaseEntity<int>
	{
		public int ParentId {
			get;
			set;
		}


		public int Type{
			get;
			set;
		}

		public string PageId{
			get;
			set;
		}

		[Required(ErrorMessage = "名称不能为空！")]
		public string Name{
			get;
			set;
		}

		public string Description{
			get;
			set;
		}

		public int Sequence{
			get;
			set;
		}

		public string Url{
			get;
			set;
		}
	}
}
