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

		public async Task<List<Personnel>> GetPersonnels(){
			var result = await _personnelRepository.Get().ToListAsync();
			return result;
		}

		public async Task<PaginatedList<Personnel>> GetPersonnelsBy(PersonnelQuery personnelQuery)
		{
			var pagedList = await _personnelRepository.GetPersonnels(personnelQuery);

			if (personnelQuery.PageSize * (personnelQuery.PageIndex - 1) >= pagedList.Count) {
				personnelQuery.PageIndex = (int)Math.Ceiling(((double)pagedList.Count) / personnelQuery.PageSize);
				pagedList = await _personnelRepository.GetPersonnels(personnelQuery);
			}
			return pagedList;
		}

		public void InsertPersonnel(Personnel personnel){
			personnel.Status = (int)Status.有效;
			personnel.CreateTime = DateTime.Now;

			_personnelRepository.Insert(personnel);
			_personnelRepository.Save();
			
		}

		public void AbandonPersonnel(int[] ids)
		{
			var personnels = _personnelRepository.Get(p=>ids.Contains(p.Id));
			foreach(var entity in personnels){
				entity.Status = (int)Status.作废;
				_personnelRepository.Update(entity);
			}
			_personnelRepository.Save();
		}

		public void UpdatePersonnel(Personnel personnel){
			_personnelRepository.Update(personnel);
			_personnelRepository.Save();
		}
	}
}
