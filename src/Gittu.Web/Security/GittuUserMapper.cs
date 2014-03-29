using System;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace Gittu.Web.Security
{
	public class GittuUserMapper : IUserMapper
	{
		public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
		{
			throw new NotImplementedException();
		}
	}
}