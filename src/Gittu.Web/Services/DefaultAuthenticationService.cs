using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Gittu.Web.Domain;
using Gittu.Web.Domain.Entities;
using Gittu.Web.Security;

namespace Gittu.Web.Services
{
	public class DefaultAuthenticationService:IAuthenticationService
	{
		public IGittuContext GittuContext { get; set; }
		public IHasher Hasher { get; set; }

		private static readonly Func<LoginResult> _invalidUPResultFn = () =>  new LoginResult
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
				return _invalidUPResultFn();
			}

			var user = GittuContext.Users.FirstOrDefault(a => a.UserName == userName);
			if (user == null)
			{
				return _invalidUPResultFn();
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

			var toReturn = _invalidUPResultFn();
			toReturn.WasUserFound = true;
			return toReturn;
		}

		//Poormans Token Store
		private static readonly Dictionary<string, Guid> TokenDictionary = new Dictionary<string, Guid>(); 
		public void SaveUserToken(string userName, Guid token)
		{
			TokenDictionary[userName.ToLower()] = token;
		}

		public User GetUserFromToken(Guid token)
		{
			var entry = TokenDictionary.FirstOrDefault(x => x.Value == token);
			if (entry.Equals(default(KeyValuePair<string,Guid>)))
			{
				return null;
			}
			return  GittuContext.Users.FirstOrDefault(a =>a.UserName == entry.Key);
		}
	}
}