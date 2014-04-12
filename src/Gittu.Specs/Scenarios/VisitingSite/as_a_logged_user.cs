using Gittu.Web.Modules;
using Machine.Specifications;
using Nancy.Testing;

namespace Gittu.Specs.Scenarios.VisitingSite
{
	public class as_a_logged_user : When_visiting_default_page 
	{
		private Establish context = () =>
		{
			_bootstrapper = new ConfigurableBootstrapper(with =>
			{
				with.Module<DefaultModule>();
				with.ViewFactory<TestingViewFactory>();
				with.ApplicationStartup((ioc, pipelines) =>
				{
					pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
					{
						ctx.CurrentUser = new DummyUser();
						return null;
					});

				});
			});
			_browser = new Browser(_bootstrapper);
		};

		private Because of = () => _response = _browser.Get("/", with =>
		{
			with.HttpRequest();
		});

		private It should_return_site_user_view = () =>
			_response.GetViewName().ShouldEqual("Home");
	}
}