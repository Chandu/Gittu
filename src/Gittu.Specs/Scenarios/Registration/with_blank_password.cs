using Machine.Specifications;

namespace Gittu.Specs.Scenarios.Registration
{
	public class with_blank_password : When_registering
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
}