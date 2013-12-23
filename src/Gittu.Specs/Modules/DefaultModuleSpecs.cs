using System;
using System.Collections.Generic;
using System.Linq;
using Gittu.Web.Modules;
using Machine.Specifications;
using Nancy;
using Nancy.Security;
using Nancy.Testing;
namespace Gittu.Specs.Modules
{
	[Subject("Default Page/Module")]
	public class when_visiting_default_page
	{
		static Browser browser = null;
		static ConfigurableBootstrapper bootstrapper = null;
		static BrowserResponse response = null;

		public class user_is_guest_user
		{
			Establish context = () =>
			{
				bootstrapper = new ConfigurableBootstrapper(with =>
				{
					with.Module<DefaultModule>();
				});
				browser = new Browser(bootstrapper);
			};

			Because of = () => response = browser.Get("/", with =>
			{
				with.HttpRequest();
			});

			It should_return_guest_user_view = () => response.Body["#sign-up"].ShouldExist();
		}

		public class user_is_site_user
		{
			Establish context = () =>
			{
				bootstrapper = new ConfigurableBootstrapper(with =>
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
				browser = new Browser(bootstrapper);
			};

			Because of = () => response = browser.Get("/", with =>
			{
				with.HttpRequest();
			});

			It should_return_site_user_view = () => response.Body["#user-profile"].ShouldExist();
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