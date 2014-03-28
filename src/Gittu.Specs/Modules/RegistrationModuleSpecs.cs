using Gittu.Web.Domain.Entities;
using Gittu.Web.Mapping;
using Gittu.Web.Modules;
using Gittu.Web.Services;
using Gittu.Web.ViewModels;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace Gittu.Specs.Modules
{
	public class RegistrationModuleSpecs
	{
		protected static Browser _browser;
		protected static ConfigurableBootstrapper _bootstrapper;
		protected static BrowserResponse _response;

		protected Establish context = () =>
		{
			_bootstrapper = new ConfigurableBootstrapper(with =>
			{
				var registrationServiceMock = new Moq.Mock<IRegistrationService>();
				with.Dependency(registrationServiceMock.Object);
				with.Module<RegisterModule>();
			});
			_browser = new Browser(_bootstrapper);
		};

		protected static void ShouldHaveErroredWith(string message)
		{
			_response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
			var result = _response.Body.DeserializeJson<InvalidInputResponse>();
			result.Messages.ShouldNotBeEmpty();
			result.Messages.Values.ShouldContain(message);
		}
	}

	[Subject("Registration")]
	public class When_register_url_is_requested : RegistrationModuleSpecs
	{
		private Because of = () =>
		 _response = _browser.Get("/register", with => with.HttpRequest());
	 
		private It should_return_register_view = () => 
			_response.Body["#register-panel"].ShouldExist();
	}

	[Subject("Registration")]
	public class With_blank_email : RegistrationModuleSpecs
	{
		private Because of = () =>
		{
			_response = _browser.Post("/register", with =>
			{
				with.HttpRequest();
				with.FormValue("email", "");
				with.FormValue("password", "lalalala");
				with.FormValue("confirmpassword", "lalalala");
				with.FormValue("username", "bloke");
			});
		};

		private It should_return_email_is_required_message = () => 
			ShouldHaveErroredWith("Email address is required.");
	}

	[Subject("Registration")]
	public class With_blank_username : RegistrationModuleSpecs
	{
		private Because of = () =>
		{
			_response = _browser.Post("/register", with =>
			{
				with.HttpRequest();
				with.FormValue("email", "c@gmail.com");
				with.FormValue("password", "lalalala");
				with.FormValue("confirmpassword", "lalalala");
				with.FormValue("username", "");
			});
		};

		private It should_return_user_name_is_required_message = () => 
			ShouldHaveErroredWith("Username is required.");
	}

	[Subject("Registration")]
	public class With_blank_password : RegistrationModuleSpecs
	{
		private Because of = () =>
		{
			_response = _browser.Post("/register", with =>
			{
				with.HttpRequest();
				with.FormValue("email", "c@gmail.com");
				with.FormValue("password", "");
				with.FormValue("confirmpassword", "lalalala");
				with.FormValue("username", "chandu");
			});
		};

		private It should_return_password_is_required_message = () => 
			ShouldHaveErroredWith("Password is required.");
	}

	[Subject("Registration")]
	public class With_mismatching_password_and_confirm_password : RegistrationModuleSpecs
	{
		private Because of = () =>
		{
			_response = _browser.Post("/register", with =>
			{
				with.HttpRequest();
				with.FormValue("email", "c@gmail.com");
				with.FormValue("password", "jajajajaja");
				with.FormValue("confirmpassword", "asdasd");
				with.FormValue("username", "chandu");
			});
		};

		private It should_return_invalid_password_combination_message = () => 
			ShouldHaveErroredWith("Password and Confirm Password donot match.");
	}

	[Subject("Registration")]
	public class With_valid_registration_data : RegistrationModuleSpecs
	{

		private Establish context = () =>
			{
				_bootstrapper = new ConfigurableBootstrapper(with =>
				{
					var registrationServiceMock = new Moq.Mock<IRegistrationService>();
					registrationServiceMock
						.Setup(svc => svc.Register(Moq.It.IsAny<User>(), Moq.It.IsAny<string>()))
						.Returns(new RegistrationResult(true, string.Empty));
				AutoMapper.Mapper.AddProfile<RegisterViewModelProfile>();
					with.Dependency(registrationServiceMock.Object);
					with.Module<RegisterModule>();
				});
				_browser = new Browser(_bootstrapper);
			};

		private Because of = () =>
		{
			_response = _browser.Post("/register", with =>
			{
				with.HttpRequest();
				with.FormValue("email", "c@gmail.com");
				with.FormValue("password", "jajajajaja");
				with.FormValue("confirmpassword", "jajajajaja");
				with.FormValue("username", "chandu");
			});
		};

		private It should_redirect_to_login_page = () =>
		{
			_response.StatusCode.ShouldEqual(HttpStatusCode.SeeOther);
			_response.ShouldHaveRedirectedTo("login");
		};
	}
}