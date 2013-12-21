using System;
using System.Collections.Generic;

namespace Gittu.Web.ViewModels
{
	public class RepositoryViewModel
	{
		public RepositoryViewModel()
		{
			Branches = new List<string>();
		}
		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime LastCommitDate { get; set; }

		public IList<string> Branches { get; set; }

		public string RepoUrl { get; set; }
	}
}