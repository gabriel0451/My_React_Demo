using System;
using System.ComponentModel.DataAnnotations;

namespace My_Web_API_Entity
{
	public class BaseEntity<T> where T : struct 
	{
		[Key]
		public T Id{
			get;
			set;
		}

		public T Status{
			get;
			set;
		}

		//[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		//public DateTime CreateTime{
		//	get;
		//	set;
		//}
	}
}
