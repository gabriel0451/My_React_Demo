using System;
using Autofac;
using My_Web_API_Service_Contract;

namespace My_Web_API_Service
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterAssemblyTypes(this.ThisAssembly)
				.Where(t => t.IsAssignableTo<IService>())
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
	}
}
