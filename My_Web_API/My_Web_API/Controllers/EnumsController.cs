using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using My_Web_API.Model;
using Microsoft.Extensions.Options;

namespace My_Web_API.Controllers
{
	[Route("[controller]")]
	public class EnumsController : Controller
	{
		private readonly EnumsConfig _enums;

		public EnumsController(IOptions<EnumsConfig> options)
		{
			_enums = options.Value;
		}

		[Authorize(Roles = "Manage")]
		[HttpPost("GetEnumsByKey")]
		public IActionResult Get([FromBody]string[] keys)
		{
			bool success = false;
			string message = "";
			var enums = _enums.Enums.Where(p => keys.Contains(p.EnumName));
			if (!enums.Any()) {
				message = "字典项不存在！";
			} else {
				success = true;
			}

			return Ok(new {
				success,
				message,
				enums
			});
		}
	}
}
