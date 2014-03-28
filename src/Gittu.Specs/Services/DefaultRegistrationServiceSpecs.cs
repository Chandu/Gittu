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
		protected static Mock<IUnitOfWork> _uowMock;
		protected static Mock<IGittuContext> _gittuContext;
		Establish context = () =>
		{
			_gittuContext = new Mock<IGittuContext>();
			_uowMock  = new Mock<IUnitOfWork>();
			_registrationService = new DefaultRegistrationService(_uowMock.Object, _gittuContext.Object);
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
			static User _user;

			Establish context = () =>
			{
				_user = new User
					{
						EMail = "test@gmail.com",
						Id = 1,
						UserName = "chandu"
					};
				_uowMock = new Mock<IUnitOfWork>();
				_uowMock
					.Setup(a => a.Attach(_user))
					.Verifiable();
				_registrationService = new DefaultRegistrationService(_uowMock.Object, _gittuContext.Object);
			};

			Because of = () =>
				_registrationResult  = _registrationService.Register(_user, "password");
		

			It should_hash_the_password_for_storage = () =>
			{
				_user.Password.ShouldNotBeEmpty();
				_user.Salt.ShouldNotBeEmpty();
			};

			It should_have_called_UnitOfWork_Attach = () =>
				_uowMock.Verify();

			It should_register_the_user_successfully = () => 
				_registrationResult.IsSuccess.ShouldEqual(true);
		}
	}
}