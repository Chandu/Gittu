using Machine.Specifications;

namespace Gittu.Specs.Scenarios.Registration
{
	public class with_blank_username : When_registering
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
}