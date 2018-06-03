using System;
using System.ComponentModel.DataAnnotations;

namespace My_Web_API_Entity
{
	/// <summary>
    /// 聚合根
    /// </summary>
	public interface IAggregateRoot: IEntity
	{
		/// <summary>
		/// 版本号(乐观锁)
		/// </summary>
		[Timestamp]
		byte[] RowVersion { get; set; }

		int Status { get; set; }
	}

	/// <summary>
	/// 聚合根
	/// </summary>
	/// <typeparam name="TKey">标识类型</typeparam>
	public interface IAggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot
	{		
	}
}
