using LibGit2Sharp;
using System.Linq;
namespace Gittu.Web.Services
{
	public interface IRepositoryProvider
	{
		IRepository Get(string repositoryPath, RepositoryOptions options);
		IRepository Get(string repositoryPath);
		IQueryable<IRepository> GetAll(string basePath);
	}
}