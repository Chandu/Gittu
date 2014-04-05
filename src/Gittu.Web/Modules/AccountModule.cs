using Gittu.Web.ViewModels;
using Nancy;
using Nancy.Authentication.Forms;

namespace Gittu.Web.Modules
{
	public class AccountModule : NancyModule
	{
		public AccountModule():base("account")
		{
			Get["login"] = _ => View["Login", new LoginViewModel()];
			Get["register"] = _ => View["Register", new RegisterViewModel()];
			Get["logout"] = _ =>  this.LogoutAndRedirect("~/");

		}
		
	}
}