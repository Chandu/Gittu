using System;

namespace Gittu.Web.Services
{
	public interface IUserTokenStore
	{
		void Save(string userName, Guid token);
		string Get(Guid token);
	}
}