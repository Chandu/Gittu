using System;
using System.Collections.Generic;
using System.Linq;
using Gittu.Specs.Helpers;
using Gittu.Web.Modules;
using Nancy.Security;
using Nancy.Testing;
using NSpec;

namespace Gittu.Specs.Modules
{
	internal class DefaultModuleSpecs : nspec
	{
		private Action Stop = () => System.Diagnostics.Debugger.Launch();

		private Action<Nancy.Testing.ConfigurableBootstrapper.ConfigurableBootstrapperConfigurator> DefaultConfiguration = with =>
		{
			with.Module<DefaultModule>();
			with.RootPathProvider<TestRootPathProvider>();
		};

		private void when_default_url_is_requested()
		{
			context["user is not logged in"] = () =>
			{
				var bootstrapper = new ConfigurableBootstrapper(with =>
				{
					DefaultConfiguration(with);
				});

				var browser = new Browser(bootstrapper);

				var response = browser.Get("/", with =>
				{
					with.HttpRequest();
				});

				it["returns guest user view"] = () =>
				{
					response.Body["#sign-up"].ShouldExist();
				};
			};

			context["user is logged in"] = () =>
			{
				var bootstrapper = new ConfigurableBootstrapper(with =>
				{
					DefaultConfiguration(with);
					with.ApplicationStartup((ioc, pipelines) =>
					{
						pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
								{
									ctx.CurrentUser = new DummyUser();
									return null;
								});
					});
				});

				var browser = new Browser(bootstrapper);
				var response = browser.Get("/", with =>
				{
					with.HttpRequest();
				});

				it["returns  user home view"] = () =>
				{
					response.Body["#user-profile"].ShouldExist();
				};
			};
		}

		private class DummyUser : IUserIdentity
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