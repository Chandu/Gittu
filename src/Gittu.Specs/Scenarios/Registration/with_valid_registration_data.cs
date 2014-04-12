using Gittu.Web.Domain.Entities;
using Gittu.Web.Mapping;
using Gittu.Web.Modules;
using Gittu.Web.Services;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace Gittu.Specs.Scenarios.Registration
{
	public class with_valid_registration_data : When_registering
	{
		private static User _newUser;
		private Establish context = () =>
		{
			_bootstrapper = new ConfigurableBootstrapper(with =>
			{
				_registrationServiceMock = new Moq.Mock<IRegistrationService>();
				_registrationServiceMock
					.Setup(svc => svc.Register(Moq.It.IsAny<User>(), Moq.It.IsAny<string>()))
					.Returns(new RegistrationResult(true, string.Empty))
					.Callback<User, string>((a, b) =>
					{
						_newUser = a;
					});
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
}