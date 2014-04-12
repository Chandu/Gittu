using System;
using Gittu.Web.Services;
using Machine.Specifications;
using Nancy.Testing;

namespace Gittu.Specs.Scenarios.LoggingIn
{
	public class with_valid_credentials : When_logging_in
	{
		private Establish context = () =>
		{
			_authenticationServiceMock
				.Setup(a => a.Validate(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
				.Returns(new LoginResult(true, "Login successful."))
				.Verifiable();
		};

		private Because of = () => _response = _browser.Post("/account/login", with =>
		{
			with.HttpRequest();
			with.FormValue("userName", "username");
			with.FormValue("password", "password");
		});

		private It should_have_called_authentication_service = () =>
			_authenticationServiceMock.Verify();

		private It should_redirect_to_home_page = () =>
			_response.ShouldHaveRedirectedTo("/");

		private It should_generate_and_save_guid_token_for_user = () =>
		{
			_userTokenStore.Get("userName").ShouldNotBeNull();
			_userTokenStore.Get("userName").ShouldNotEqual(Guid.Empty);
		};

	}
}