using System;
using System.Data.Common;

namespace My_Web_API_EF.Contract
{
	public interface IUnitOfWork
	{
		DbTransaction BeginTransaction();

		void CommitTransaction();

		void RollbackTransaction();
	}
}
