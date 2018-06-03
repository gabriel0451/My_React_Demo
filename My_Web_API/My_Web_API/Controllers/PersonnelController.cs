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
using My_Web_API_Entity;
using My_Web_API_Entity.QueryEntity;
using My_Web_API_Service_Contract;

namespace My_Web_API.Controllers
{
	[Route("[controller]")]
	public class PersonnelController : Controller
	{

		private readonly IPersonnelService _personnelService;
		private readonly JwtSetting _setting;

		public PersonnelController(IPersonnelService personnelService, IOptions<JwtSetting> options)
		{
			_personnelService = personnelService;
			_setting = options.Value;
		}
		// GET api/values
		[HttpPost("GetPersonnelByUser")]
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
					new Claim(ClaimTypes.Role, "Manage")
				};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.SecretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				_setting.Issuer,
				_setting.Audience,
				claims,
				DateTime.Now,
				DateTime.Now.AddMinutes(1),
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

		[Authorize(Roles ="Manage")]
		[HttpGet("GetPersonnels")]
		public IActionResult GetPersonnels()
		{
			//var claim = HttpContext.User.Identities.SelectMany(p => p.Claims).Where(o => o.Type == ClaimTypes.Name).FirstOrDefault();
			var personnels = _personnelService.GetPersonnels();
			return Ok(new {
				success = true,
				data= new{
					count=personnels.Result.Count(),
					items=personnels.Result
				} 
			});
		}

		[Authorize(Roles = "Manage")]
		[HttpPost("GetPersonnelByConditions")]
		public async Task<PaginatedList<Personnel>> GetPersonnelsByConditions([FromBody]PersonnelQuery conditions){
			var pagedList = await _personnelService.GetPersonnelsBy(conditions);
			return pagedList;
		}

		[Authorize(Roles = "Manage")]
		[HttpPost("InsertPersonnel")]
		public IActionResult InsertPersonnel([FromBody]Personnel personnel)
		{
			try {
				_personnelService.InsertPersonnel(personnel);

				return Ok(new {
					success = true,
					message = "保存成功！"
				});
			} catch (Exception ex) {
				return Ok(new {
					success = false,
					message = ex.Message
				});
  			}
		}

		[Authorize(Roles = "Manage")]
		[HttpPut("AbandonPersonnel")]
		public IActionResult AbandonPersonnel([FromBody]int[] ids){
			{
				try {
					_personnelService.AbandonPersonnel(ids);
					return Ok(new {
						success = true,
						message = "作废成功！"
					});
				} catch (Exception ex) {
					return Ok(new {
						success = false,
						message = ex.Message
					});
				}
			}
		}

		[Authorize(Roles = "Manage")]
		[HttpPut("UpdatePersonnel")]
		public IActionResult UpdatePersonnel([FromBody]Personnel personnel)
		{
			{
				try {

					_personnelService.UpdatePersonnel(personnel);
					return Ok(new {
						success = true,
						message = "保存成功！"
					});
				} catch (Exception ex) {
					return Ok(new {
						success = false,
						message = ex.Message
					});
				}
			}
		}


	}
}
