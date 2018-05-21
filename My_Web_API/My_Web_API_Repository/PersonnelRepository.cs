using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using My_Web_API_EF;
using My_Web_API_Entity;
using My_Web_API_Repository_Contract;

namespace My_Web_API_EF_Repository
{
	public class PersonnelRepository : Repository<Personnel>, IPersonnelRepository
	{
		public PersonnelRepository(DomainContext context)
			: base(context)
		{
		}

		public async Task<PaginatedList<Personnel>> GetPersonnels(int pageIndex, int pageSize)
		{
			var source = DbSet;
			int count = await source.CountAsync();
			List<Personnel> personnels = null;
			if (count > 0) {
				personnels = await source.OrderBy(x => x.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
			}

			return new PaginatedList<Personnel>(pageIndex, pageSize, count, personnels ?? new List<Personnel>());
		}
	}
}
