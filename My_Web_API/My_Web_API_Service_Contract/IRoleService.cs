using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My_Web_API_Entity;
using My_Web_API_Entity.QueryEntity;

namespace My_Web_API_Service_Contract
{
	public interface IRoleService : IService
	{
		Task<PaginatedList<Role>> GetRolesBy(RoleQuery conditions);

		void InsertRole(Role personnel);

		void UpdateRole(Role personnel);

		void AbandonRoles(int[] ids);
	}
}
