using System;
using Gittu.Web.Domain.Entities;
namespace Gittu.Web.Services
{
	public interface IRegistrationService
	{
		Tuple<bool, string> Register(User user, string password);
	}
}