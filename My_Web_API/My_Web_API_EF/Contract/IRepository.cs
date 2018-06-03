using System;
using System.Linq;
using System.Linq.Expressions;
using My_Web_API_Entity;

namespace My_Web_API_EF.Contract
{
	/// <summary>
	/// Repository标记接口
	/// </summary>
	public interface IRepository
	{
	}


	public interface IRepository<TEntity> : IRepository 
		where TEntity : AggregateRoot 
	{
		IQueryable<TEntity> Get(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "");

		TEntity GetByID(object id);

		void Insert(TEntity entity);

		void Delete(object id);

		void Delete(TEntity entityToDelete);

		void Update(TEntity entityToUpdate);

		void Save();
	}
}
