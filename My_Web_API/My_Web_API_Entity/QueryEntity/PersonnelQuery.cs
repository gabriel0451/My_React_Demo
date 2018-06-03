using System;
namespace My_Web_API_Entity.QueryEntity
{
	public class PersonnelQuery
	{
		public string Name{
			get;
			set;
		}

		public string CellNumber{
			get;
			set;
		}

		public int? Status{
			get;
			set;
		}

		public DateTime? StartCreateTime{
			get;
			set;
		}

		public DateTime? EndCreateTime{
			get;
			set;
		}

		public int PageIndex {
			get;
			set;
		}

		public int PageSize {
			get;
			set;
		}
	}
}
