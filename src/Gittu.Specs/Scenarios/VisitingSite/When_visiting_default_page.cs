using Machine.Specifications;
using Nancy.Testing;

namespace Gittu.Specs.Scenarios.VisitingSite
{
	[Subject("Visiting site")]
	public class When_visiting_default_page
	{
		protected static Browser _browser;
		protected static ConfigurableBootstrapper _bootstrapper;
		protected static BrowserResponse _response;
	}
}