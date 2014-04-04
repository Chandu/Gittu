using System;
using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Services
{
	public interface IAuthenticationService
	{
		LoginResult Validate(string userName, string password);
		void SaveUserToken(string userName, Guid token);
		User GetUserFromToken(Guid token);
	}
}