using System.Collections.Generic;
using Nancy.Security;

namespace Gittu.Web.Security
{
	public class GittuUserIdentity : IUserIdentity
	{
		public IEnumerable<string> Claims { get; internal set; }

		public string UserName { get; internal set; }
	}
}