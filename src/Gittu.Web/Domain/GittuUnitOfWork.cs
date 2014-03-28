using System;
using System.Data.Entity;
using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Domain
{
	internal class GittuUnitOfWork : DbContext, IUnitOfWork
	{
		public void Commit()
		{
			SaveChanges();
		}

		public void Attach<TEntity>(TEntity entity) where TEntity : class, IEntity
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

		public IDisposable Start()
		{
			return this;
		}
	}
}