using Nancy;

namespace Gittu.Web.Modules
{
	public class DefaultModule:NancyModule
	{
		public DefaultModule()
		{
			Get["/"] = _ => View["Home.html", new {Name="Chandu"}];
		}
	}
}