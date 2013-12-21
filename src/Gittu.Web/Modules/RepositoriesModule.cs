using System;
using System.Collections.Generic;
using System.Linq;
using Gittu.Web.Services;
using Gittu.Web.ViewModels;
using Nancy;

namespace Gittu.Web.Modules
{
	public class RepositoriesModule:NancyModule
	{
		public IRepositoryProvider RepositoryProvider { get; set; }

		public RepositoriesModule(IRepositoryProvider repositoryProvider):base("repositories")
		{
			RepositoryProvider = repositoryProvider;
			Get["/"] = _ =>
			{
				var model = RepositoryProvider
					.GetAll(@"G:\Github\")
					.Select(repo => new RepositoryViewModel
					{
						Name = "",
						Description = repo.Commits.Any()?repo.Commits.LastOrDefault().MessageShort:"",
						Branches = repo.Branches.Any()?repo.Branches.Select(a => a.Name).ToList():new List<string>(),
						LastCommitDate = DateTime.Now
					}).ToList();
					return Response.AsJson(model);
			};
		}
	}
}