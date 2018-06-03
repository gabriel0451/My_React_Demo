using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace My_Web_API_EF.Infrustures
{
	public class DynamicEntity
	{
		private Dictionary<object, object> _attrs;
		public DynamicEntity()
		{
			_attrs = new Dictionary<object, object>();
		}
		public DynamicEntity(Dictionary<object, object> dic)
		{
			_attrs = dic;
		}
		public static DynamicEntity Parse(object obj)
		{
			DynamicEntity model = new DynamicEntity();
			foreach (PropertyInfo info in obj.GetType().GetProperties()) {
				model._attrs.Add(info.Name, info.GetValue(obj, null));
			}
			return model;
		}
		public T GetValue<T>(string field)
		{
			object obj2 = null;
			if (!_attrs.TryGetValue(field, out obj2)) {
				_attrs.Add(field, default(T));
			}
			if (obj2 == null) {
				return default(T);
			}
			return (T)obj2;
		}

		public void SetValue<T>(string field, T value)
		{
			if (_attrs.ContainsKey(field)) {
				_attrs[field] = value;
			} else {
				_attrs.Add(field, value);
			}
		}

		[JsonIgnore]
		public Dictionary<object, object> Attrs {
			get {
				return _attrs;
			}
		}
		//提供索引方式操作属性值
		public object this[string key] {
			get {
				object obj2 = null;
				if (_attrs.TryGetValue(key, out obj2)) {
					return obj2;
				}
				return null;
			}
			set {
				if (_attrs.Any(m => string.Compare(m.Key.ToString(), key, true) != -1)) {
					_attrs[key] = value;
				} else {
					_attrs.Add(key, value);
				}
			}
		}
		[JsonIgnore]
		public string[] Keys {
			get {
				return _attrs.Keys.Select(m => m.ToString()).ToArray();
			}
		}

		[Key]
		public int Id {
			get {
				return GetValue<int>("Id");
			}
			set {
				SetValue("Id", value);
			}
		}
		[Timestamp]
		[JsonIgnore]
		public byte[] Version { get; set; }
	}
}
