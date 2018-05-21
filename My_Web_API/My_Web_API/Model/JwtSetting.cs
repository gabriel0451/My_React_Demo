using System;
namespace My_Web_API.Model
{
	public class JwtSetting
	{
		/// <summary>
		/// 证书颁发者
		/// </summary>
		public string Issuer { get; set; }

		/// <summary>
		/// 允许使用的角色
		/// </summary>
		public string Audience { get; set; }

		/// <summary>
		/// 加密字符串
		/// </summary>
		public string SecretKey { get; set; }
	}
}
