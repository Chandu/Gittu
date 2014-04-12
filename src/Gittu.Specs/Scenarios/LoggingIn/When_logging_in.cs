using System;
using Gittu.Web.Modules;
using Gittu.Web.Security;
using Gittu.Web.Services;
using Machine.Specifications;
using Moq;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Testing;

namespace Gittu.Specs.Scenarios.LoggingIn
{
	[Subject("Logging in")]
	public class When_logging_in
	{
		protected static Browser _browser;
		protected static ConfigurableBootstrapper _bootstrapper;
		protected static BrowserResponse _response;
		protected static Mock<IAuthenticationService> _authenticationServiceMock;
		protected static InMemoryUserTokenStore _userTokenStore;
		protected static Mock<IUserMapper> _userMapperMock;

		private Establish context = () =>
		{
			_authenticationServiceMock = new Mock<IAuthenticationService>();
			_userTokenStore = new InMemoryUserTokenStore();
			_userMapperMock = new Mock<IUserMapper>();

			var config = new FormsAuthenticationConfiguration()
			{
				RedirectUrl = "~/login",
				UserMapper = _userMapperMock.Object
			};
			_userMapperMock
				.Setup(a => a.GetUserFromIdentifier(Moq.It.IsAny<Guid>(), Moq.It.IsAny<NancyContext>()))
				.Returns(() => new GittuUserIdentity())
				;
			var fakePipelines = new Pipelines();
			FormsAuthentication.Enable(fakePipelines, config);

			_bootstrapper = new ConfigurableBootstrapper(with =>
			{
				with.Dependency(_authenticationServiceMock.Object);
				with.Dependency(_userTokenStore);
				with.Module<LoginModule>();
				with.ViewFactory<TestingViewFactory>();
			});
			_browser = new Browser(_bootstrapper);
		};

	}
}