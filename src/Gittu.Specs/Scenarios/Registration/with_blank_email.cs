using Machine.Specifications;

namespace Gittu.Specs.Scenarios.Registration
{
	public class with_blank_email : When_registering
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
			_response.ShouldHaveErroredWith("EMail address is required.");
	}
}