using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace My_Web_API.Infrustures
{
	public class MyTokenValidate : ISecurityTokenValidator
	{
		public bool CanValidateToken => true;

		public int MaximumTokenSizeInBytes { get; set; }

		public bool CanReadToken(string securityToken)
		{
			return true;
		}

		public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
		{
			ClaimsPrincipal principal;
			try {
				validatedToken = null;
				var token = new JwtSecurityToken(securityToken);
				//获取到Token的一切信息
				var payload = token.Payload;
				var role = (from t in payload where t.Key == ClaimTypes.Role select t.Value).FirstOrDefault();
				var name = (from t in payload where t.Key == ClaimTypes.Name select t.Value).FirstOrDefault();
				var issuer = token.Issuer; 
				var key = token.SecurityKey;
				var audience = token.Audiences;
				var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
				identity.AddClaim(new Claim(ClaimTypes.Name, name.ToString()));
				identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
				principal = new ClaimsPrincipal(identity);
			} catch {
				validatedToken = null;                  
				principal = null;
			}
			return principal;
		}
	}
}
