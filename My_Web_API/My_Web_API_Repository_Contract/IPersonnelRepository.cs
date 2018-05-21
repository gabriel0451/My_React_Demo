using System;
using System.Threading.Tasks;
using My_Web_API_EF.Contract;
using My_Web_API_Entity;

namespace My_Web_API_Repository_Contract
{
	public interface IPersonnelRepository : IRepository<Personnel>
	{
		Task<PaginatedList<Personnel>> GetPersonnels(int pageIndex, int pageSize);
	}
}
