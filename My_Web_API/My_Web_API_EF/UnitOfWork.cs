using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using My_Web_API_EF.Contract;

namespace My_Web_API_EF
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _context;

		public UnitOfWork(DbContext context)
		{
			_context = context;
		}

		public DbTransaction BeginTransaction()
		{
			_context.Database.BeginTransaction();

			return _context.Database.CurrentTransaction.GetDbTransaction();
		}

		public void CommitTransaction()
		{
			_context.SaveChanges();
			_context.Database.CommitTransaction();
		}

		public void RollbackTransaction()
		{
			_context.Database.RollbackTransaction();
		}
	}
}
