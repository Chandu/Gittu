using System;
using System.Data.Entity;
using System.Linq;
using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Domain
{
	public class GittuContext : DbContext, IGittuContext, IUnitOfWork
	{
		public GittuContext(string connectionName)
			: base(connectionName)
		{
		}

		private DbSet<Repository> Repositories { get { return Set<Repository>(); } }
		private DbSet<User> Users { get { return Set<User>(); } }

		IQueryable<Repository> IGittuContext.Repositories
		{
			get { return Repositories; }
		}

		IQueryable<User> IGittuContext.Users
		{
			get { return Users; }
		}

		public IDisposable Start()
		{
			return this;
		}

		void IUnitOfWork.Commit()
		{
			SaveChanges();
		}

		void IUnitOfWork.Attach<TEntity>(TEntity entity)
		{
			if (entity == null)
				throw new ArgumentException("Cannot attach a null entity");
			var entry = Entry(entity);
			if (entry.Entity.IsNew())
			{
				entry.State = EntityState.Added;
			}

			switch (entry.State)
			{
				case EntityState.Added: Set<TEntity>().Add(entity); break;
				case EntityState.Detached: Set<TEntity>().Attach(entity); break;
				case EntityState.Deleted: Set<TEntity>().Remove(entity); break;
				default: break;
			}
		}
	}
}