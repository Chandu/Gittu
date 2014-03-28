using Gittu.Web.Modules;
using Gittu.Web.Services;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Testing;
using It = Machine.Specifications.It;

namespace Gittu.Specs.Modules
{
	[Subject("Logging in")]
	public class When_logging_in
	{
		private static Browser _browser;
		private static ConfigurableBootstrapper _bootstrapper;
		private static BrowserResponse _response;
		private static Mock<IAuthenticationService> _authenticationServiceMock;

		private Establish context = () =>
		{
			_authenticationServiceMock = new Mock<IAuthenticationService>();
			_bootstrapper = new ConfigurableBootstrapper(with =>
			{
				with.Dependency(_authenticationServiceMock.Object);
				with.Module<LoginModule>();
			});
			_browser = new Browser(_bootstrapper);
		};

		public class with_blank_username
		{
			private Because of = () => _response = _browser.Post("/login", with =>
			{
				with.HttpRequest();
				with.FormValue("userName", "");
				with.FormValue("password", "somepassword");
			});

			private It should_respond_with_bad_request_status = () =>
				_response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

			private It should_return_the_message_username_is_required = () =>
				_response.ShouldHaveErroredWith("Username is required.");
		}

		public class with_invalid_username_length
		{
			private Because of = () => _response = _browser.Post("/login", with =>
			{
				with.HttpRequest();
				with.FormValue("userName", "ja");
				with.FormValue("password", "somepassword");
			});

			private It should_respond_with_bad_request_status = () =>
				_response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

			private It should_return_the_message_username_is_invalid = () =>
				_response.ShouldHaveErroredWith("Username is invalid.");
		}

		public class with_blank_password
		{
			private Because of = () => _response = _browser.Post("/login", with =>
			{
				with.HttpRequest();
				with.FormValue("userName", "someUser");
				with.FormValue("password", "");
			});

			private It should_respond_with_bad_request_status = () =>
				_response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

			private It should_return_the_message_password_is_required = () =>
				_response.ShouldHaveErroredWith("Password is required.");
		}

		public class with_invalid_password_length
		{
			private Because of = () => _response = _browser.Post("/login", with =>
			{
				with.HttpRequest();
				with.FormValue("userName", "jajaja");
				with.FormValue("password", "so");
			});

			private It should_respond_with_bad_request_status = () =>
				_response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

			private It should_return_the_message_password_is_invalid = () =>
				_response.ShouldHaveErroredWith("Password is invalid.");
		}

		public class with_valid_credentials
		{
			private Establish context = () => _authenticationServiceMock
				.Setup(a => a.Validate(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
				.Returns(new LoginResult(true, "Login successful."))
				.Verifiable();

			private Because of = () => _response = _browser.Post("/login", with =>
			{
				with.HttpRequest();
				with.FormValue("userName", "username");
				with.FormValue("password", "password");
			});

			private It should_have_called_authentication_service = () =>
				_authenticationServiceMock.Verify();

			private It should_redirect_to_home_page = () =>
				_response.ShouldHaveRedirectedTo("/");
		}

		public class with_invalid_username_password_combination
		{
			private Establish context = () => _authenticationServiceMock
				.Setup(a => a.Validate(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
				.Returns(new LoginResult(false, "Invalid password"))
				.Verifiable();

			private Because of = () => _response = _browser.Post("/login", with =>
			{
				with.HttpRequest();
				with.FormValue("userName", "username");
				with.FormValue("password", "wrong-password");
			});

			private It should_have_called_authentication_service = () =>
	_authenticationServiceMock.Verify();

			private It should_respond_with_unauthorized = () =>
				_response.StatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
		}
	}
}