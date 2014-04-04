using Gittu.Web.Domain.Entities;
using Gittu.Web.Exceptions;
using Gittu.Web.Mapping;
using Gittu.Web.Modules;
using Gittu.Web.Services;
using Gittu.Web.ViewModels;
using Machine.Specifications;
using Nancy;
using Nancy.Extensions;
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
				with.ViewFactory<TestingViewFactory>();
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
			_response.ShouldHaveErroredWith<RegisterViewModel>("Email address is required.");
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
			_response.ShouldHaveErroredWith<RegisterViewModel>("Username is required.");
	}

	[Subject("Registration")]
	public class With_out_accepting_terms : RegistrationModuleSpecs
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

		private It should_return_message_should_agree_with_terms = () =>
			_response.ShouldHaveErroredWith<RegisterViewModel>("Please read and Agree to our Terms & Conditions to complete the registration.");
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
			_response.ShouldHaveErroredWith<RegisterViewModel>("Password is required.");
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
			_response.ShouldHaveErroredWith<RegisterViewModel>("Password and Confirm Password donot match.");
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
				with.FormValue("termsAgreed", "true");
			});
		};

		private It should_successfully_register_the_user = () =>
			_response.StatusCode.ShouldEqual(HttpStatusCode.SeeOther);


		private It should_redirect_to_login_page = () =>
			_response.ShouldHaveRedirectedTo("login");
	}

	[Subject("Registration")]
	public class with_duplicate_username:RegistrationModuleSpecs
	{
		private Establish context = () =>
		{
			_bootstrapper = new ConfigurableBootstrapper(with =>
			{
				_registrationServiceMock = new Moq.Mock<IRegistrationService>();
				_registrationServiceMock
					.Setup(svc => svc.Register(Moq.It.IsAny<User>(), Moq.It.IsAny<string>()))
					.Throws(new UsernameExistsException("chandu"));
				AutoMapper.Mapper.AddProfile<RegisterViewModelProfile>();
				with.Dependency(_registrationServiceMock.Object);
				with.Module<RegisterModule>();
				with.ViewFactory<TestingViewFactory>();
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
				with.FormValue("termsAgreed", "true");
			});
		};


		private It should_return_BadRequest_status = () =>
			_response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

		private It should_return_username_exists_message = () =>
			_response.ShouldHaveErroredWith<RegisterViewModel>("A user with the username chandu already exists in the system.");

	}

	[Subject("Registration")]
	public class with_duplicate_email : RegistrationModuleSpecs
	{
		private Establish context = () =>
		{
			_bootstrapper = new ConfigurableBootstrapper(with =>
			{
				_registrationServiceMock = new Moq.Mock<IRegistrationService>();
				_registrationServiceMock
					.Setup(svc => svc.Register(Moq.It.IsAny<User>(), Moq.It.IsAny<string>()))
					.Throws(new EMailExistsException("c@gmail.com"));
				AutoMapper.Mapper.AddProfile<RegisterViewModelProfile>();
				with.Dependency(_registrationServiceMock.Object);
				with.Module<RegisterModule>();
				with.ViewFactory<TestingViewFactory>();
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
				with.FormValue("termsAgreed", "true");
			});
		};


		private It should_return_BadRequest_status = () =>
			_response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

		private It should_return_username_exists_message = () =>
			_response.ShouldHaveErroredWith<RegisterViewModel>("A user with the email c@gmail.com already exists in the system.");

	}

}