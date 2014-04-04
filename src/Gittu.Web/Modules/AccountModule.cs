using Nancy;

namespace Gittu.Web.Modules
{
	public class AccountModule : NancyModule
	{
		public AccountModule():base("account")
		{
			Get["login"] = _ => View["Login"];
			Get["register"] = _ => View["Register"];

		}
		
	}
}