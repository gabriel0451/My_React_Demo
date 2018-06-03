using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using My_Web_API_EF.Contract;
using My_Web_API_Entity;
using My_Web_API_Entity.QueryEntity;

namespace My_Web_API_Repository_Contract
{
	public interface IPersonnelRepository : IRepository<Personnel>
	{
		Task<PaginatedList<Personnel>> GetPersonnels(PersonnelQuery personnelQuery);
	}
}
