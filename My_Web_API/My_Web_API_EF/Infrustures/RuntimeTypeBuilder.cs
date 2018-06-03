using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace My_Web_API_EF.Infrustures
{
	public static class RuntimeTypeBuilder
	{
		private static ModuleBuilder moduleBuilder;
		static RuntimeTypeBuilder()
		{
			AssemblyName an = new AssemblyName("__RuntimeType");
			moduleBuilder = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run).DefineDynamicModule("__RuntimeType");
		}
		public static Type Build(TypeMeta meta)
		{
			TypeBuilder builder = moduleBuilder.DefineType(meta.TypeName, TypeAttributes.Public);
			CustomAttributeBuilder tableAttributeBuilder = new CustomAttributeBuilder(typeof(TableAttribute).GetConstructor(new Type[1] { typeof(string) }), new object[] { "RuntimeModel_" + meta.TypeName });
			builder.SetParent(meta.BaseType);
			builder.SetCustomAttribute(tableAttributeBuilder);

			foreach (var item in meta.PropertyMetas) {
				AddProperty(item, builder, meta.BaseType);
			}
			return builder.CreateTypeInfo().UnderlyingSystemType;
		}

		private static void AddProperty(TypeMeta.TypePropertyMeta property, TypeBuilder builder, Type baseType)
		{
			PropertyBuilder propertyBuilder = builder.DefineProperty(property.PropertyName, PropertyAttributes.None, property.PropertyType, null);

			foreach (var item in property.AttributeMetas) {
				if (item.ConstructorArgTypes == null) {
					item.ConstructorArgTypes = new Type[0];
					item.ConstructorArgValues = new object[0];
				}
				ConstructorInfo cInfo = item.AttributeType.GetConstructor(item.ConstructorArgTypes);
				PropertyInfo[] pInfos = item.Properties.Select(m => item.AttributeType.GetProperty(m)).ToArray();
				CustomAttributeBuilder aBuilder = new CustomAttributeBuilder(cInfo, item.ConstructorArgValues, pInfos, item.PropertyValues);
				propertyBuilder.SetCustomAttribute(aBuilder);
			}

			MethodAttributes attributes = MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Public;
			MethodBuilder getMethodBuilder = builder.DefineMethod("get_" + property.PropertyName, attributes, property.PropertyType, Type.EmptyTypes);
			ILGenerator iLGenerator = getMethodBuilder.GetILGenerator();
			MethodInfo getMethod = baseType.GetMethod("GetValue").MakeGenericMethod(new Type[] { property.PropertyType });
			iLGenerator.DeclareLocal(property.PropertyType);
			iLGenerator.Emit(OpCodes.Nop);
			iLGenerator.Emit(OpCodes.Ldarg_0);
			iLGenerator.Emit(OpCodes.Ldstr, property.PropertyName);
			iLGenerator.EmitCall(OpCodes.Call, getMethod, null);
			iLGenerator.Emit(OpCodes.Stloc_0);
			iLGenerator.Emit(OpCodes.Ldloc_0);
			iLGenerator.Emit(OpCodes.Ret);
			MethodInfo setMethod = baseType.GetMethod("SetValue").MakeGenericMethod(new Type[] { property.PropertyType });
			MethodBuilder setMethodBuilder = builder.DefineMethod("set_" + property.PropertyName, attributes, null, new Type[] { property.PropertyType });
			ILGenerator generator2 = setMethodBuilder.GetILGenerator();
			generator2.Emit(OpCodes.Nop);
			generator2.Emit(OpCodes.Ldarg_0);
			generator2.Emit(OpCodes.Ldstr, property.PropertyName);
			generator2.Emit(OpCodes.Ldarg_1);
			generator2.EmitCall(OpCodes.Call, setMethod, null);
			generator2.Emit(OpCodes.Nop);
			generator2.Emit(OpCodes.Ret);
			propertyBuilder.SetGetMethod(getMethodBuilder);
			propertyBuilder.SetSetMethod(setMethodBuilder);

		}
	}
}
