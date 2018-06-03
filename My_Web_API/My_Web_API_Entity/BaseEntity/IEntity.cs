using System;
namespace My_Web_API_Entity
{
	/// <summary>
    /// 实体
    /// </summary>
	public interface IEntity
	{
	}
	/// <summary>
    /// 实体
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
	public interface IEntity<TKey> : IEntity
	{
		/// <summary>
		/// 标识
		/// </summary>
		TKey Id { get; set; }
	}
}
