using System;
using My_Web_API_Repository_Contract;
using System.Linq;
using My_Web_API_Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using My_Web_API_Service_Contract;

namespace My_Web_API_Service
{
	public class PageService : IPageService
	{
		private readonly IPageRepository _pageRepository;

		public PageService(IPageRepository pageRepository)
		{
			_pageRepository = pageRepository;
		}

		public async Task<List<Page>> GetPages()
		{
			var result = await _pageRepository.Get().ToListAsync();
			return result;
		}

	}
}
