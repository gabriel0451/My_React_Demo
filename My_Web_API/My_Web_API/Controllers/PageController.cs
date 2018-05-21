using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using My_Web_API_Service_Contract;

namespace My_Web_API.Controllers
{
	[Route("[controller]")]
	public class PageController : Controller
	{
		private readonly IPageService _pageService;


		public PageController(IPageService pageService)
		{
			_pageService = pageService;
		}

		[HttpGet("GetPages")]
		public IActionResult GetPages()
		{
			var pages = _pageService.GetPages();

			return Ok(new {
				success = true,
				data = pages.Result
			});
		}
	}
}
