using System.Collections.Generic;
using Nancy.Security;

namespace Gittu.Web.Security
{
	public class GittuUserIdentity:IUserIdentity
	{
		public IEnumerable<string> Claims
		{
			get { throw new System.NotImplementedException(); }
		}

		public string UserName
		{
			get { throw new System.NotImplementedException(); }
		}
	}
}