using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My_Web_API_Entity;

namespace My_Web_API_Service_Contract
{
	public interface IPageService: IService
	{
		Task<List<Page>> GetPages();
	}
}
