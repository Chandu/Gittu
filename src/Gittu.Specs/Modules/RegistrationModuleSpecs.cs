using Gittu.Web.Domain.Entities;
using Gittu.Web.Mapping;
using Gittu.Web.Modules;
using Gittu.Web.Services;
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
		protected static Moq.Mock<IRegistrationService> _registrationServiceMock;
		protected Establish context = () =>
		{
			_bootstrapper = new ConfigurableBootstrapper(with =>
			{
				_registrationServiceMock = new Moq.Mock<IRegistrationService>();
				with.Dependency(_registrationServiceMock.Object);
				with.Module<RegisterModule>();
			});
			_browser = new Browser(_bootstrapper);
		};
	}

	[Subject("Registration")]
	public class With_blank_email : RegistrationModuleSpecs
	{
		private Because of = () =>
		{
			_response = _browser.Post("/account/register", with =>
			{
				with.HttpRequest();
				with.FormValue("email", "");
				with.FormValue("password", "lalalala");
				with.FormValue("confirmpassword", "lalalala");
				with.FormValue("username", "bloke");
			});
		};

		private It should_return_email_is_required_message = () =>
			_response.ShouldHaveErroredWith("Email address is required.");
	}

	[Subject("Registration")]
	public class With_blank_username : RegistrationModuleSpecs
	{
		private Because of = () =>
		{
			_response = _browser.Post("/account/register", with =>
			{
				with.HttpRequest();
				with.FormValue("email", "c@gmail.com");
				with.FormValue("password", "lalalala");
				with.FormValue("confirmpassword", "lalalala");
				with.FormValue("username", "");
			});
		};

		private It should_return_user_name_is_required_message = () =>
			_response.ShouldHaveErroredWith("Username is required.");
	}

	[Subject("Registration")]
	public class With_blank_password : RegistrationModuleSpecs
	{
		private Because of = () =>
		{
			_response = _browser.Post("/account/register", with =>
			{
				with.HttpRequest();
				with.FormValue("email", "c@gmail.com");
				with.FormValue("password", "");
				with.FormValue("confirmpassword", "lalalala");
				with.FormValue("username", "chandu");
			});
		};

		private It should_return_password_is_required_message = () =>
			_response.ShouldHaveErroredWith("Password is required.");
	}

	[Subject("Registration")]
	public class With_mismatching_password_and_confirm_password : RegistrationModuleSpecs
	{
		private Because of = () =>
		{
			_response = _browser.Post("/account/register", with =>
			{
				with.HttpRequest();
				with.FormValue("email", "c@gmail.com");
				with.FormValue("password", "jajajajaja");
				with.FormValue("confirmpassword", "asdasd");
				with.FormValue("username", "chandu");
			});
		};

		private It should_return_invalid_password_combination_message = () =>
			_response.ShouldHaveErroredWith("Password and Confirm Password donot match.");
	}

	[Subject("Registration")]
	public class With_valid_registration_data : RegistrationModuleSpecs
	{
		private Establish context = () =>
			{
				_bootstrapper = new ConfigurableBootstrapper(with =>
				{
					_registrationServiceMock = new Moq.Mock<IRegistrationService>();
					_registrationServiceMock
						.Setup(svc => svc.Register(Moq.It.IsAny<User>(), Moq.It.IsAny<string>()))
						.Returns(new RegistrationResult(true, string.Empty));
					AutoMapper.Mapper.AddProfile<RegisterViewModelProfile>();
					with.Dependency(_registrationServiceMock.Object);
					with.Module<RegisterModule>();
				});
				_browser = new Browser(_bootstrapper);
			};

		private Because of = () =>
		{
			_response = _browser.Post("/account/register", with =>
			{
				with.HttpRequest();
				with.FormValue("email", "c@gmail.com");
				with.FormValue("password", "jajajajaja");
				with.FormValue("confirmpassword", "jajajajaja");
				with.FormValue("username", "chandu");
			});
		};

		private It should_successfully_register_the_user = () =>
			_response.StatusCode.ShouldEqual(HttpStatusCode.SeeOther);

		
		private It should_redirect_to_login_page = () =>
			_response.ShouldHaveRedirectedTo("login");
	}
}