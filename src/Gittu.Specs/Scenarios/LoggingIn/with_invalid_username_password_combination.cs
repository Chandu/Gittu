using Gittu.Web.Services;
using Machine.Specifications;
using Nancy;
using It = Machine.Specifications.It;

namespace Gittu.Specs.Scenarios.LoggingIn
{
	public class with_invalid_username_password_combination : When_logging_in
	{
		private Establish context = () => _authenticationServiceMock
			.Setup(a => a.Validate(Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
			.Returns(new LoginResult(false, "Invalid password"))
			.Verifiable();

		private Because of = () => _response = _browser.Post("/account/login", with =>
		{
			with.HttpRequest();
			with.FormValue("userName", "username");
			with.FormValue("password", "wrong-password");
		});

		private It should_have_called_authentication_service = () =>
_authenticationServiceMock.Verify();

		//TODO: (CV) Shouldn't it be Unauthorized?
		private It should_respond_with_bad_request = () =>
			_response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
	}

}