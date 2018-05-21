using System;
using Autofac;
using My_Web_API_EF;
using My_Web_API_EF.Contract;

namespace My_Web_API_Repository
{
	public class RepositoryModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<UnitOfWork>()
				.As<IUnitOfWork>()
				.InstancePerLifetimeScope();
			builder.RegisterAssemblyTypes(this.ThisAssembly)
				.Where(t => t.IsAssignableTo<IRepository>())
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();
		}
	}
}
