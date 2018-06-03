using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My_Web_API_Entity;
using My_Web_API_Entity.QueryEntity;

namespace My_Web_API_Service_Contract
{
	public interface IPersonnelService : IService
	{
		Task<List<Personnel>> GetPersonnelsBy(string loginId, string password);

		Task<List<Personnel>> GetPersonnels();

		Task<PaginatedList<Personnel>> GetPersonnelsBy(PersonnelQuery personnelQuery);

		void InsertPersonnel(Personnel personnel);

		void UpdatePersonnel(Personnel personnel);

		void AbandonPersonnel(int[] ids);
	}
}
