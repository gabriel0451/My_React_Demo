using System;
using My_Web_API_Repository_Contract;
using System.Linq;
using My_Web_API_Entity;
using My_Web_API_Service_Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace My_Web_API_Service
{
	public class PersonnelService : IPersonnelService
	{
		private readonly IPersonnelRepository _personnelRepository;

		public PersonnelService(IPersonnelRepository personnelRepository)
		{
			_personnelRepository = personnelRepository;
		}

		public async Task<List<Personnel>> GetPersonnelsBy(string loginId, string password)
		{
			var result = await _personnelRepository.Get(p => p.LoginId == loginId && p.Password == password).ToListAsync();
			return result; 
		}

		public async Task<Personnel> GetPersonnelByLoginId(string loginId){
			var result = await _personnelRepository.Get(p => p.LoginId == loginId).FirstOrDefaultAsync();
			return result;
		}

	}
}
