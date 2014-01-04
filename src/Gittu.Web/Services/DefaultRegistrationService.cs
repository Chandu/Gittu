using System;
using System.Linq;
using Gittu.Web.Domain;
using Gittu.Web.Domain.Entities;
using Gittu.Web.Exceptions;

namespace Gittu.Web.Services
{
	public class DefaultRegistrationService : IRegistrationService
	{
		public IGittuContext GittuContext { get; set; }

		public DefaultRegistrationService(IGittuContext gittuContext)
		{
			GittuContext = gittuContext;
		}

		public Tuple<bool, string> Register(User user, string password)
		{
			if(user == null)
			{
				throw new ArgumentException("User argument cannot be null", "user");
			}
			if(string.IsNullOrEmpty(password))
			{
				throw new ArgumentException("Password argument cannot be null", "password");
			}
			if(GittuContext.Users.Any(a => a.UserName == user.UserName))
			{
				throw new UserNameExistsException();
			}
			throw new NotImplementedException();
		}
	}
}