using System.Collections.Generic;
using System.Linq;
using Gittu.Web.Modules;
using Machine.Specifications;
using Nancy.Security;
using Nancy.Testing;

namespace Gittu.Specs.Modules
{
	[Subject("Visiting Default Page")]
	public class When_visiting_default_page
	{
		private static Browser _browser;
		private static ConfigurableBootstrapper _bootstrapper;
		private static BrowserResponse _response;

		public class As_a_guest_user
		{
			private Establish context = () =>
			{
				_bootstrapper = new ConfigurableBootstrapper(with =>
				{
					with.Module<DefaultModule>();
				});
				_browser = new Browser(_bootstrapper);
			};

			private Because of = () => _response = _browser.Get("/", with =>
			{
				with.HttpRequest();
			});

			private It should_return_guest_user_view = () =>
				_response.Body["#sign-up"].ShouldExist();
		}

		public class As_a_logged_user
		{
			private Establish context = () =>
			{
				_bootstrapper = new ConfigurableBootstrapper(with =>
				{
					with.Module<DefaultModule>();
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
				_response.Body["#user-profile"].ShouldExist();
		}

		public class DummyUser : IUserIdentity
		{
			public IEnumerable<string> Claims
			{
				get { return Enumerable.Empty<string>(); }
			}

			public string UserName
			{
				get { return string.Empty; }
			}
		}
	}
}