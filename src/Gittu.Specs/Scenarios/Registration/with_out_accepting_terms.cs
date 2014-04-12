using Machine.Specifications;

namespace Gittu.Specs.Scenarios.Registration
{
	public class with_out_accepting_terms : When_registering
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
			_response.ShouldHaveErroredWith("Please read and Agree to our Terms & Conditions to complete the registration.");
	}
}