﻿using System;
using System.Data.Entity;
using Gittu.Web.Domain.Entities;
using Gittu.Web.Domain.Entities.Mapping;

namespace Gittu.Web.Domain
{
	internal class GittuUnitOfWork : DbContext, IUnitOfWork
	{
		static GittuUnitOfWork()
		{
			Database.SetInitializer<GittuUnitOfWork>(null);
		}

		public IEntityMappingsConfigurator EntityMappingsConfigurator { get; set; }

		public GittuUnitOfWork(string connectionName):this(connectionName, new EntityMappingsConfigurator())
		{
		}

		public GittuUnitOfWork(string connectionName, IEntityMappingsConfigurator entityMappingsConfigurator)
			: base(connectionName)
		{
			EntityMappingsConfigurator = entityMappingsConfigurator;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			EntityMappingsConfigurator.Configure(modelBuilder);
		}


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