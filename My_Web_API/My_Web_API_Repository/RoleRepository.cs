using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using My_Web_API_EF;
using My_Web_API_Entity;
using My_Web_API_Entity.QueryEntity;
using My_Web_API_Repository_Contract;

namespace My_Web_API_EF_Repository
{
	public class RoleRepository : Repository<Role>, IRoleRepository
	{
		public RoleRepository(DomainContext context)
			: base(context)
		{
		}

		public async Task<PaginatedList<Role>> GetRoles(RoleQuery conditions)
		{
			IQueryable<Role> source = DbSet;
			if (conditions != null) {
				if (!string.IsNullOrEmpty(conditions.Name))
					source = source.Where(p => p.Name.ToLower().Contains(conditions.Name.ToLower()));
				if (conditions.IsAdmin.HasValue)
					source = source.Where(p => p.IsAdmin == conditions.IsAdmin);
				if (conditions.Status.HasValue)
					source = source.Where(p => p.Status == conditions.Status);
				if (conditions.StartCreateTime.HasValue)
					source = source.Where(p => p.CreateTime >= conditions.StartCreateTime);
				if (conditions.EndCreateTime.HasValue)
					source = source.Where(p => p.CreateTime <= conditions.EndCreateTime);
			}

			int count = await source.CountAsync();
			List<Role> personnels = null;
			if (count > 0) {
				personnels = await source.OrderBy(x => x.Id).Skip((conditions.PageIndex - 1) * conditions.PageSize).Take(conditions.PageSize).ToListAsync();
			}

			return new PaginatedList<Role>(conditions.PageIndex, conditions.PageSize, count, personnels ?? new List<Role>());
		}
	}
}
