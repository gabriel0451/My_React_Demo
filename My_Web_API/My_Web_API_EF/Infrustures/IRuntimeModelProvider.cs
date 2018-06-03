using System;
namespace My_Web_API_EF.Infrustures
{
	public interface IRuntimeModelProvider
	{
		Type GetType(int modelId);
		Type[] GetTypes();
	}
}
