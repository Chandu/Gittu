using Machine.Specifications;

namespace Gittu.Specs.Scenarios.Registration
{
	public class with_mismatching_password_and_confirm_password : When_registering
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
}