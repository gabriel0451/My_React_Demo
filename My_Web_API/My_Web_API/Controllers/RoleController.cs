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
	public class RoleController : Controller
	{

		private readonly IRoleService _roleService;

		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		[Authorize(Roles = "Manage")]
		[HttpPost("GetRolesByConditions")]
		public async Task<PaginatedList<Role>> GetPersonnelsByConditions([FromBody]RoleQuery conditions)
		{
			var pagedList = await _roleService.GetRolesBy(conditions);
			return pagedList;
		}

		[Authorize(Roles = "Manage")]
		[HttpPost("InsertRole")]
		public IActionResult InsertRole([FromBody]Role role)
		{
			try {
				var currentUser = HttpContext.User.Identities.SelectMany(p => p.Claims).Where(o => o.Type == ClaimTypes.Name).FirstOrDefault();
				role.CreatorName = currentUser.Value;
				_roleService.InsertRole(role);

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
		[HttpPut("AbandonRoles")]
		public IActionResult AbandonRoles([FromBody]int[] ids)
		{
			{
				try {
					_roleService.AbandonRoles(ids);
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
		[HttpPut("UpdateRole")]
		public IActionResult UpdateRole([FromBody]Role role)
		{
			{
				try {

					_roleService.UpdateRole(role);
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
