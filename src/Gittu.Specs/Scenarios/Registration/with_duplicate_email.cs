using Gittu.Web.Domain.Entities;
using Gittu.Web.Exceptions;
using Gittu.Web.Mapping;
using Gittu.Web.Modules;
using Gittu.Web.Services;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace Gittu.Specs.Scenarios.Registration
{
	[Subject("Registration")]
	public class with_duplicate_email : When_registering
	{
		private Establish context = () =>
		{
			_bootstrapper = new ConfigurableBootstrapper(with =>
			{
				_registrationServiceMock = new Moq.Mock<IRegistrationService>();
				_registrationServiceMock
					.Setup(svc => svc.Register(Moq.It.IsAny<User>(), Moq.It.IsAny<string>()))
					.Throws(new EMailExistsException("c@gmail.com"));
				AutoMapper.Mapper.AddProfile<RegisterViewModelProfile>();
				with.Dependency(_registrationServiceMock.Object);
				with.Module<RegisterModule>();
				with.ViewFactory<TestingViewFactory>();
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


		private It should_return_BadRequest_status = () =>
			_response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);

		private It should_return_username_exists_message = () =>
			_response.ShouldHaveErroredWith("A user with the email c@gmail.com already exists in the system.");

	}
}