using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using My_Web_API.Model;
using My_Web_API_Repository_Contract;
using My_Web_API_Service_Contract;

namespace My_Web_API.Controllers
{
	[Route("[controller]")]
	public class PersonnelController : Controller
	{
		
		private readonly IPersonnelService _personnelService;
		private readonly JwtSetting _setting;

		public PersonnelController(IPersonnelService serviceRepository,IOptions<JwtSetting> options)
		{
			_personnelService = serviceRepository;
			_setting = options.Value;
		}
		// GET api/values
		[HttpPost("GetPersonnelBy")]
		public IActionResult GetPersonnelBy([FromBody]UserViewModel user)
		{
			if (string.IsNullOrEmpty(user.LoginId) || string.IsNullOrEmpty(user.Password)) {
				return Ok(new {
					success = false,
					message = "用户名密码不能为空！"
				});
			}
			var personnel = _personnelService.GetPersonnelsBy(user.LoginId, user.Password).Result.FirstOrDefault();
			if (personnel == null) {
				return Ok(new {
					success = false,
					message = "用户名或者密码错误！"
				});
			}
			var claims = new Claim[] {
					new Claim(ClaimTypes.Name, user.LoginId),
					new Claim(ClaimTypes.Role, "admin, Manage")
				};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.SecretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				_setting.Issuer,
				_setting.Audience,
				claims,
				DateTime.Now,
				DateTime.Now.AddMinutes(30),
				creds);
			return Ok(new {
				success = true,
				user = new {
					personnel.Name,
					personnel.CellNumber,
					personnel.Email,
					personnel.Photo
				},
				Token = new JwtSecurityTokenHandler().WriteToken(token)
			});
		}

		[Authorize(Roles = "admin")]
		[HttpPost("GetPersonnelByToken")]
		public IActionResult GetPersonnelByToken()
		{
			var claim = HttpContext.User.Identities.SelectMany(p => p.Claims).Where(o => o.Type == ClaimTypes.Name).FirstOrDefault();
			if (claim == null)
				return Ok(new {	
					success = false,
					message = "您无权访问！"
				});
			var personnel = _personnelService.GetPersonnelByLoginId(claim.Value);
			return Ok(new {
				success = true,
				user = personnel.Result
			});
		}
	}
}
