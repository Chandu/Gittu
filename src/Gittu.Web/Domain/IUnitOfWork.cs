using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Domain
{
	public interface IUnitOfWork
	{
		IDisposable Start();
		void Attach<TEntity>(TEntity entity) where TEntity : class, IEntity;
		void Commit();
	}
}
