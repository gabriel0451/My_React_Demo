using My_Web_API_EF;
using My_Web_API_Entity;
using My_Web_API_Repository_Contract;

namespace My_Web_API_EF_Repository
{
	public class PageRepository : Repository<Page>, IPageRepository
	{
		public PageRepository(DomainContext context)
			: base(context)
		{
		}
	}
}
