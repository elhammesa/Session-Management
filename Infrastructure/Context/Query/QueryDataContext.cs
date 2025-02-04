using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Persistance.Extentions;

namespace Infrastructure.Context.Query
{
	public class QueryDataContext : DbContext
	{
		public QueryDataContext(DbContextOptions<QueryDataContext> options) : base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);


		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<SessionPerson>()
			 .HasOne(sp => sp.Session)
			 .WithMany(s => s.SessionPersons)
			 .HasForeignKey(sp => sp.SessionId);
			modelBuilder.Entity<SessionReport>()
		   .HasOne(sp => sp.Session)
		   .WithMany(s => s.Reports)
		   .HasForeignKey(sp => sp.SessionId);

			var assembly = typeof(IBaseEntity).Assembly;
			modelBuilder.RegisterAllEntities<IBaseEntity>(assembly);
		}
	}
}
