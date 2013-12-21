using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace Gittu.Specs.Helpers
{
	//http://www.jefclaes.be/2012/06/making-my-first-nancyfx-test-pass.html
	public class TestRootPathProvider : IRootPathProvider
	{
		private static string _cachedRootPath;

		public string GetRootPath()
		{
			if (!string.IsNullOrEmpty(_cachedRootPath))
				return _cachedRootPath;

			var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);

			bool rootPathFound = false;
			while (!rootPathFound)
			{
				var directoriesContainingViewFolder = currentDirectory.GetDirectories(
									"Views", SearchOption.AllDirectories);
				if (directoriesContainingViewFolder.Any())
				{
					_cachedRootPath = directoriesContainingViewFolder.First().FullName;
					rootPathFound = true;
				}

				currentDirectory = currentDirectory.Parent;
			}

			return _cachedRootPath;
		}
	}
}
