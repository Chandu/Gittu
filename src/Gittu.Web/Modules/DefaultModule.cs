using Nancy;

namespace Gittu.Web.Modules
{
	public class DefaultModule:NancyModule
	{
		public DefaultModule()
		{
			Get["/"] = _ =>
			{
				if (Context.CurrentUser != null)
				{
					return View["Home.html"];
				}
				return View["GuestHome.html"];
			};
		}
	}
}