namespace Gittu.Web.Services
{
	public interface IAuthenticationService
	{
		LoginResult Validate(string userName, string password);
	}
}