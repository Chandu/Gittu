using Machine.Specifications;
using Nancy;

namespace Gittu.Specs.Scenarios.LoggingIn
{
	public class with_blank_username : When_logging_in
	{
		private Because of = () => _response = _browser.Post("/account/login", with =>
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
}