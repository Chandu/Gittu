using System.IO;
using System.Linq;
using LibGit2Sharp;

namespace Gittu.Web.Services
{
	public class RepositoryProvider : IRepositoryProvider
	{
		public IRepository Get(string repositoryPath, RepositoryOptions options)
		{
			return new Repository(repositoryPath, options); 
		}
		public IRepository Get(string repositoryPath)
		{
			return Get(repositoryPath, null);
		}

		public IQueryable<IRepository> GetAll(string basePath)
		{
			return Directory
				.GetDirectories(basePath)
				.Where(a => Directory.Exists(Path.Combine(a, ".git")))
				.Select(Get)
				.AsQueryable();
		}
	}
}