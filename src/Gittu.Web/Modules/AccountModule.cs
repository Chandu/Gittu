using Nancy;

namespace Gittu.Web.Modules
{
	public class AccountModule : NancyModule
	{
		public AccountModule():base("account")
		{
			Get["/"] = _ => View["LoginOrSignup"];
		}
		
	}
}