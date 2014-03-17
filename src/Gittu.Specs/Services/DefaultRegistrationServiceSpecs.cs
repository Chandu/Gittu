using System;
using System.Linq;
using Gittu.Web.Domain;
using Gittu.Web.Domain.Entities;
using Gittu.Web.Exceptions;
using Gittu.Web.Services;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Gittu.Specs.Services
{
	[Subject("Default Registration service")]
	public abstract class DefaultRegistrationServiceSpecs
	{
		protected static DefaultRegistrationService RegistrationService;

		Establish context = () =>
		{
			var gittuContextMock = new Mock<IGittuContext>();
			var uowMock = new Mock<IUnitOfWork>();
			RegistrationService = new DefaultRegistrationService(uowMock.Object, gittuContextMock.Object);
		};

		Cleanup after = () =>
		{
			
		};
	}

	[Subject(typeof(DefaultRegistrationService), "When register method is called")]
	public class When_register_method_is_called:DefaultRegistrationServiceSpecs
	{
		public class With_null_as_user_parameter
		{
			static Exception argumentException;

			Because of = () =>
			{
				argumentException = Catch.Exception(() => RegistrationService.Register(null, "somepassword"));
			};

			It should_throw_user_argument_error = () =>
			{
				argumentException.ShouldNotBeNull();
				argumentException.ShouldBeOfType<ArgumentException>();
				(argumentException as ArgumentException).ParamName.ShouldEqual("user");
			};
		}

		public class With_blank_string_as_password
		{
			static Exception argumentException;

			Because of = () =>
			{
				argumentException = Catch.Exception(() => RegistrationService.Register(new User(), ""));
			};

			It should_throw_password_argument_error = () =>
			{
				argumentException.ShouldNotBeNull();
				argumentException.ShouldBeOfType<ArgumentException>();
				(argumentException as ArgumentException).ParamName.ShouldEqual("password");
			};
		}

		public class With_already_used_UserName
		{
			static Exception userNameExistsException;

			Establish context = () =>
			{
				var gittuContextMock = new Mock<IGittuContext>();
				var uowMock = new Mock<IUnitOfWork>();
				var dummyUsers = new[]
				{
					new User
					{
						EMail = "test@gmail.com",
						Id = 1,
						UserName = "chandu"
					}
				};
				gittuContextMock.Setup(a => a.Users).Returns(dummyUsers.AsQueryable());
				RegistrationService = new DefaultRegistrationService(uowMock.Object, gittuContextMock.Object);
			};

			Because of = () => 
			{
				userNameExistsException = Catch.Exception(() => RegistrationService.Register(new User
				{
					UserName="chandu",
					EMail="some@gmail.com"
				}, "somepassword"));			
			};
			It should_throw_user_exists_exception = () => 
				userNameExistsException.ShouldBeOfType<UserNameExistsException>();
		}

		public class With_valid_registration_information
		{
			static RegistrationResult registrationResult;
			Because of = () =>
			{
				registrationResult  = RegistrationService.Register(new User
					{
						EMail = "test@gmail.com",
						Id = 1,
						UserName = "chandu"
					}, "password");
			};
			It should_register_the_user_successfully = () => registrationResult.IsSuccess.ShouldEqual(true);
		}
	}
}