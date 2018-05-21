using System;
using Microsoft.EntityFrameworkCore;
using My_Web_API_Entity;

namespace My_Web_API_EF
{
	public class DomainContext: DbContext
	{
		public DomainContext(DbContextOptions<DomainContext> options)
			: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Page>().ToTable("Page");
			modelBuilder.Entity<Personnel>().ToTable("Personnel");
		}
	}
}
