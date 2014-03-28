using System.Data.Entity;
using System.Linq;
using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Domain
{
	internal class GittuContext : DbContext, IGittuContext
	{
		public GittuContext(string connectionName)
			: base(connectionName)
		{
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
	}
}