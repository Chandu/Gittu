using Gittu.Web.ViewModels;
using Nancy;

namespace Gittu.Web.Modules
{
	public class DefaultModule:NancyModule
	{
		public DefaultModule()
		{
			Get["/"] = _ => {
				if (Context.CurrentUser != null)
				{
					return View["Home.html"];
				}
				else
				{
					return View["GuestHome.html"];
				}
			};
		}
	}
}