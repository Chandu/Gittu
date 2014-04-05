using System;
using System.Linq;
using Gittu.Web.Domain;
using Gittu.Web.Services;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace Gittu.Web.Security
{
	public class GittuUserMapper : IUserMapper
	{
		private readonly IUserTokenStore _userTokenStore;

		public GittuUserMapper(IUserTokenStore userTokenStore)
		{
			_userTokenStore = userTokenStore;
		}

		public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
		{
			var user = _userTokenStore.Get(identifier);
			if (string.IsNullOrEmpty(user))
			{
				return null;
			}
			return new GittuUserIdentity
			{
				UserName = user,
				Claims = Enumerable.Empty<string>()
			};
		}
	}
}