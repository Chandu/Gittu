using System.Linq;
using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Domain
{
	public interface IGittuContext
	{
		IQueryable<Repository> Repositories { get;  }
	}
}