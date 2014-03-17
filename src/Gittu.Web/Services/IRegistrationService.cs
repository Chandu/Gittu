using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Services
{
	public interface IRegistrationService
	{
		RegistrationResult Register(User user, string password);
	}
}