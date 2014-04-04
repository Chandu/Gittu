using System;
using System.Linq;
using Gittu.Web.Services;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace Gittu.Web.Security
{
	public class GittuUserMapper : IUserMapper
	{
		private readonly IAuthenticationService _authenticationService;

		public GittuUserMapper(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
		{
			var user = _authenticationService.GetUserFromToken(identifier);
			if (user == null)
			{
				return null;
			}
			return new GittuUserIdentity
			{
				UserName = user.UserName,
				Claims = Enumerable.Empty<string>()
			};
		}
	}
}