using System;
using System.Linq;
using Gittu.Web.Domain;
using Gittu.Web.Security;

namespace Gittu.Web.Services
{
	public class DefaultAuthenticationService : IAuthenticationService
	{
		public IGittuContext GittuContext { get; set; }

		public IHasher Hasher { get; set; }

		private static readonly Func<LoginResult> InvalidUpResultFn = () => new LoginResult
				{
					IsSuccess = false,
					Message = "Invalid username/password."
				};

		public DefaultAuthenticationService(IGittuContext gittuContext, IHasher hasher)
		{
			GittuContext = gittuContext;
			Hasher = hasher;
		}

		public LoginResult Validate(string userName, string password)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
			{
				return InvalidUpResultFn();
			}

			var user = GittuContext.Users.FirstOrDefault(a => a.UserName == userName);
			if (user == null)
			{
				return InvalidUpResultFn();
			}
			if (user.ValidatePassword(password, Hasher))
			{
				return new LoginResult
				{
					IsSuccess = true,
					Message = "Validated successfully.",
					WasUserFound = true
				};
			}

			var toReturn = InvalidUpResultFn();
			toReturn.WasUserFound = true;
			return toReturn;
		}
	}
}