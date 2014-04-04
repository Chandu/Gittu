using Nancy;

namespace Gittu.Web.Modules
{
	public class DefaultModule:NancyModule
	{
		public DefaultModule()
		{
			Get["/"] = _ => Context.CurrentUser != null ? View["Home"] : View["GuestHome"];
		}
	}
}