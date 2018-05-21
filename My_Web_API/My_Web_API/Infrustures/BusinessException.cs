using System;
namespace My_Web_API.Infrustures
{
	public class BusinessException : Exception
	{
		public BusinessException(int hResult, string message)
			: base(message)
		{
			base.HResult = hResult;
		}
	}
}
