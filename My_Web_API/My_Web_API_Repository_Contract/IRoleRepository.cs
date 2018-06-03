using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using My_Web_API_EF.Contract;
using My_Web_API_Entity;
using My_Web_API_Entity.QueryEntity;

namespace My_Web_API_Repository_Contract
{
	public interface IRoleRepository : IRepository<Role>
	{
		Task<PaginatedList<Role>> GetRoles(RoleQuery conditions);
	}
}
