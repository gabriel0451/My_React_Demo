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
	public class PersonnelRepository : Repository<Personnel>, IPersonnelRepository
	{
		public PersonnelRepository(DomainContext context)
			: base(context)
		{
		}

		public async Task<PaginatedList<Personnel>> GetPersonnels(PersonnelQuery personnelQuery)
		{
			IQueryable<Personnel> source = DbSet;
			if (personnelQuery != null) {
				if (!string.IsNullOrEmpty(personnelQuery.Name))
					source = source.Where(p => p.Name.ToLower().Contains(personnelQuery.Name.ToLower()));
				if (!string.IsNullOrEmpty(personnelQuery.CellNumber))
					source = source.Where(p => p.CellNumber.ToLower().Contains(personnelQuery.CellNumber.ToLower()));
				if (personnelQuery.Status.HasValue)
					source = source.Where(p => p.Status == personnelQuery.Status);
				if (personnelQuery.StartCreateTime.HasValue)
					source = source.Where(p => p.CreateTime >= personnelQuery.StartCreateTime);
				if (personnelQuery.EndCreateTime.HasValue)
					source = source.Where(p => p.CreateTime <= personnelQuery.EndCreateTime);
			}

			int count = await source.CountAsync();
			List<Personnel> personnels = null;
			if (count > 0) {
				personnels = await source.OrderBy(x => x.Id).Skip((personnelQuery.PageIndex - 1) * personnelQuery.PageSize).Take(personnelQuery.PageSize).ToListAsync();
			}

			return new PaginatedList<Personnel>(personnelQuery.PageIndex, personnelQuery.PageSize, count, personnels ?? new List<Personnel>());
		}
	}
}
