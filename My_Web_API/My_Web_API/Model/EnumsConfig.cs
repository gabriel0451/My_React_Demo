using System;
namespace My_Web_API.Model
{
	public class EnumsConfig
	{
		public Enum[] Enums{
			get;
			set;
		}
	}

	public class Enum{
		public string EnumName {
			get;
			set;
		}

		public KeyValue[] KeyValues {
			get;
			set;
		}
	}

	public class KeyValue
	{
		public string Text {
			get;
			set;
		}

		public int Value {
			get;
			set;
		}
	}
}
