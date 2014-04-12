using Gittu.Web.Modules;
using Gittu.Web.Services;
using Machine.Specifications;
using Nancy.Testing;

namespace Gittu.Specs.Scenarios.Registration
{
	[Subject("Registering")]
	public class When_registering
	{
		protected static Browser _browser;
		protected static ConfigurableBootstrapper _bootstrapper;
		protected static BrowserResponse _response;
		protected static Moq.Mock<IRegistrationService> _registrationServiceMock;
		protected Establish context = () =>
		{
			_bootstrapper = new ConfigurableBootstrapper(with =>
			{
				_registrationServiceMock = new Moq.Mock<IRegistrationService>();
				with.Dependency(_registrationServiceMock.Object);
				with.Module<RegisterModule>();
				with.ViewFactory<TestingViewFactory>();
			});
			_browser = new Browser(_bootstrapper);
		};
	}
}