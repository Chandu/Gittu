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
		protected static DefaultRegistrationService _registrationService;

		Establish context = () =>
		{
			var gittuContextMock = new Mock<IGittuContext>();
			var uowMock = new Mock<IUnitOfWork>();
			_registrationService = new DefaultRegistrationService(uowMock.Object, gittuContextMock.Object);
		};

		Cleanup after = () =>
		{
			
		};
	}

	[Subject(typeof(DefaultRegistrationService), ".Register")]
	public class When_register_method_is_called:DefaultRegistrationServiceSpecs
	{
		public class With_null_as_user_parameter
		{
			static Exception _argumentException;

			Because of = () =>
				_argumentException = Catch.Exception(() => _registrationService.Register(null, "somepassword"));
			

			It should_throw_user_argument_error = () =>
				(_argumentException as ArgumentException).ParamName.ShouldEqual("user");
			
		}

		public class when_registering_with_blank_string_as_password
		{
			static Exception _argumentException;

			Because of = () =>
				_argumentException = Catch.Exception(() => _registrationService.Register(new User(), ""));
			
			It should_throw_password_argument_error = () =>
				(_argumentException as ArgumentException).ParamName.ShouldEqual("password");
			
		}

		public class when_registering_with_duplicate_UserName
		{
			static Exception _userNameExistsException;

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
				_registrationService = new DefaultRegistrationService(uowMock.Object, gittuContextMock.Object);
			};

			Because of = () => 
			{
				_userNameExistsException = Catch.Exception(() => _registrationService.Register(new User
				{
					UserName="chandu",
					EMail="some@gmail.com"
				}, "somepassword"));			
			};
			It should_throw_user_exists_exception = () => 
				_userNameExistsException.ShouldBeOfType<DuplicateUserExistsException>();
		}

		public class when_registering_with_valid_registration_information
		{
			static RegistrationResult _registrationResult;
			Because of = () =>
			{
				_registrationResult  = _registrationService.Register(new User
					{
						EMail = "test@gmail.com",
						Id = 1,
						UserName = "chandu"
					}, "password");
			};
			It should_register_the_user_successfully = () => 
				_registrationResult.IsSuccess.ShouldEqual(true);
		}
	}
}