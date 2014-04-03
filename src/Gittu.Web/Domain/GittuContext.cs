using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Gittu.Web.Domain.Entities;
using Gittu.Web.Domain.Entities.Mapping;

namespace Gittu.Web.Domain
{
	internal class GittuContext : DbContext, IGittuContext
	{
		static GittuContext()
		{
			Database.SetInitializer<GittuContext>(null);
		}

		public IEntityMappingsConfigurator EntityMappingsConfigurator { get; set; }

		public GittuContext(string connectionName)
			: this(connectionName, new EntityMappingsConfigurator())
		{
		}

		public GittuContext(string connectionName, IEntityMappingsConfigurator entityMappingsConfigurator)
			: base(connectionName)
		{
			EntityMappingsConfigurator = entityMappingsConfigurator;
		}

		private DbSet<Repository> Repositories { get { return Set<Repository>(); } }

		private DbSet<User> Users { get { return Set<User>(); } }

		IQueryable<Repository> IGittuContext.Repositories
		{
			get { return Repositories.AsNoTracking(); }
		}

		IQueryable<User> IGittuContext.Users
		{
			get { return Users.AsNoTracking(); }
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			EntityMappingsConfigurator.Configure(modelBuilder);
		}
	}
}