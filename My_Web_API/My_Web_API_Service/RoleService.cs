using System;
using My_Web_API_Repository_Contract;
using System.Linq;
using My_Web_API_Entity;
using My_Web_API_Service_Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using My_Web_API_Entity.QueryEntity;

namespace My_Web_API_Service
{
	public class RoleService : IRoleService
	{
		private readonly IRoleRepository _roleRepository;

		public RoleService(IRoleRepository  roleRepository)
		{
			_roleRepository = roleRepository;
		}


		public async Task<PaginatedList<Role>> GetRolesBy(RoleQuery conditions)
		{
			var pagedList = await _roleRepository.GetRoles(conditions);

			if (conditions.PageSize * (conditions.PageIndex - 1) >= pagedList.Count) {
				conditions.PageIndex = (int)Math.Ceiling(((double)pagedList.Count) / conditions.PageSize);
				pagedList = await _roleRepository.GetRoles(conditions);
			}
			return pagedList;
		}

		public void InsertRole(Role role)
		{
			role.Status = (int)Status.有效;
			role.CreateTime = DateTime.Now;
			_roleRepository.Insert(role);
			_roleRepository.Save();

		}

		public void AbandonRoles(int[] ids)
		{
			var entities = _roleRepository.Get(p => ids.Contains(p.Id));
			foreach (var entity in entities) {
				entity.Status = (int)Status.作废;
				_roleRepository.Update(entity);
			}
			_roleRepository.Save();
		}

		public void UpdateRole(Role role)
		{
			_roleRepository.Update(role);
			_roleRepository.Save();
		}
	}
}
