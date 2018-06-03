using System;
using System.ComponentModel.DataAnnotations;

namespace My_Web_API_Entity
{
	public abstract class AggregateRoot<TKey> : IAggregateRoot<TKey>
	{
		[Key]
		public TKey Id { get; set; }

		/// <summary>
		/// 版本号(乐观锁)
		/// </summary>
		[Timestamp]
		public byte[] RowVersion { get; set; }

		[Required]
		public int Status { get; set; }
	}

	public abstract class AggregateRoot : AggregateRoot<int>
	{
	}
}
