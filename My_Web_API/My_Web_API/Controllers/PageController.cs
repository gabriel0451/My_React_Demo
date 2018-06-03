using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using My_Web_API.Model;
using My_Web_API_Repository_Contract;
using My_Web_API_Service_Contract;

namespace My_Web_API.Controllers
{
	[Route("[controller]")]
	public class PageController : Controller
	{
		private readonly IPageService _pageService;
		private readonly EnumsConfig _enums;
		public PageController(IPageService pageService, IOptions<EnumsConfig> options)
		{
			_pageService = pageService;
			_enums = options.Value;
		}

		[HttpGet("GetPages")]
		public IActionResult GetPages()
		{
			var pages = _pageService.GetPages();

			return Ok(new {
				success = true,
				data = pages.Result,
				_enums
			});
		}
	}
}
