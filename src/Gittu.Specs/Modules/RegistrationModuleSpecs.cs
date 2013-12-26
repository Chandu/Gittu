using Gittu.Web.Modules;
using Gittu.Web.ViewModels;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace Gittu.Specs.Modules
{
	[Subject("Registration")]
	public class When_register_url_is_requested
	{
		static Browser browser;
		static ConfigurableBootstrapper bootstrapper;
		static BrowserResponse response;

		 Establish context = () =>
		{
			bootstrapper = new ConfigurableBootstrapper(with => with.Module<RegisterModule>());
			browser = new Browser(bootstrapper);
		};

		 Because of = () =>
		{
			response = browser.Get("/register", with => with.HttpRequest());
		};

		It should_return_register_view = () => response.Body["#register-panel"].ShouldExist();
	}

	[Subject("Registration")]
	public class When_registering
	{
		static Browser browser;
		static ConfigurableBootstrapper bootstrapper;
		static BrowserResponse response;

		Establish context = () =>
			{
				bootstrapper = new ConfigurableBootstrapper(with => with.Module<RegisterModule>());
				browser = new Browser(bootstrapper);
			};

		static void ShouldHaveErroredWith(string message)
		{
			response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
			var result = response.Body.DeserializeJson<InvalidInputViewModel>();
			result.Messages.ShouldNotBeEmpty();
			result.Messages.Values.ShouldContain(message);
		}

		public class With_blank_email
		{
			Because of = () =>
			{
				response = browser.Post("/register", with =>
				{
					with.HttpRequest();
					with.FormValue("email", "");
					with.FormValue("password", "lalalala");
					with.FormValue("confirmpassword", "lalalala");
					with.FormValue("username", "bloke");
				});
			};

			It should_return_email_is_required_message = () => ShouldHaveErroredWith("The Email field is required.");
		}

		public class With_blank_username
		{
			Because of = () =>
			{
				response = browser.Post("/register", with =>
				{
					with.HttpRequest();
					with.FormValue("email", "c@gmail.com");
					with.FormValue("password", "lalalala");
					with.FormValue("confirmpassword", "lalalala");
					with.FormValue("username", "");
				});
			};

			It should_return_user_name_is_required_message = () => ShouldHaveErroredWith("The UserName field is required.");
		}

		public class With_blank_password
		{
			Because of = () =>
			{
				response = browser.Post("/register", with =>
				{
					with.HttpRequest();
					with.FormValue("email", "c@gmail.com");
					with.FormValue("password", "");
					with.FormValue("confirmpassword", "lalalala");
					with.FormValue("username", "chandu");
				});
			};

			It should_return_password_is_required_message = () => ShouldHaveErroredWith("The Password field is required.");
		}

		public class With_mismatching_password_and_confirm_password
		{
			Because of = () =>
			{
				response = browser.Post("/register", with =>
				{
					with.HttpRequest();
					with.FormValue("email", "c@gmail.com");
					with.FormValue("password", "jajajajaja");
					with.FormValue("confirmpassword", "asdasd");
					with.FormValue("username", "chandu");
				});
			};

			It should_return_invalid_password_combination_message = () => ShouldHaveErroredWith
				("Password and Confirm Password donot match.");
		}
	}
}