using System.Collections.Generic;
using System.Linq;
using Nancy.Security;

namespace Gittu.Specs.Scenarios.VisitingSite
{
	public class DummyUser : IUserIdentity
	{
		public IEnumerable<string> Claims
		{
			get { return Enumerable.Empty<string>(); }
		}

		public string UserName
		{
			get { return string.Empty; }
		}
	}
}