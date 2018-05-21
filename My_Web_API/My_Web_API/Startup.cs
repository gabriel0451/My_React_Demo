using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using My_Web_API.Infrustures;
using My_Web_API.Model;
using My_Web_API_EF;

namespace My_Web_API
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddJsonFile("autofac.json")
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }
		public IContainer Container { get; private set; }

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<DomainContext>(options =>
			                                     options.UseSqlServer(Configuration.GetConnectionString("DomainConnection")));
			services.AddScoped<DbContext>(provider => provider.GetService<DomainContext>());

			services.AddCors();

			services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));
			JwtSetting setting = new JwtSetting();
			Configuration.Bind("JwtSetting", setting);
			services.AddAuthentication(option => {
				option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(config => {
				config.SecurityTokenValidators.Clear();
				config.SecurityTokenValidators.Add(new MyTokenValidate());
				config.Events = new JwtBearerEvents() {
					OnMessageReceived = context => {
						var token = context.Request.Headers["myToken"];
						context.Token = token.FirstOrDefault();
						return Task.CompletedTask;
					}

				};
			});
			                
			services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
				.AddJsonOptions(options => options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss");

			var builder = new ContainerBuilder();
			builder.Populate(services);
			var module = new ConfigurationModule(Configuration);
			builder.RegisterModule(module);
			this.Container = builder.Build();

			return new AutofacServiceProvider(this.Container);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();


			app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
								  .AllowAnyHeader().AllowAnyMethod());
			
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseBrowserLink();
			}else{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			appLifetime.ApplicationStopped.Register(() => this.Container.Dispose());
		}
	}
}
