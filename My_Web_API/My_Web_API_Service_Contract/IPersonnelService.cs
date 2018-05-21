using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using My_Web_API_Entity;

namespace My_Web_API_Service_Contract
{
	public interface IPersonnelService : IService
	{
		Task<List<Personnel>> GetPersonnelsBy(string loginId, string password);

		Task<Personnel> GetPersonnelByLoginId(string loginId);
	}
}
