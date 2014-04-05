using System;
using System.Linq;
using Gittu.Specs.Fakes;
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
		protected static Mock<IMailService> _mailServiceMock;
		protected static IQueryable<User> _fakeUsers = Enumerable.Empty<User>().AsQueryable();

		Establish context = () =>
		{
			_gittuContext = new Mock<IGittuContext>();
			_uowMock  = new Mock<IUnitOfWork>();
			_mailServiceMock = new Mock<IMailService>();
			_gittuContext
				.Setup(a => a.Users)
				.Returns(() => _fakeUsers);

			_registrationService = new DefaultRegistrationService(_uowMock.Object, _gittuContext.Object, _mailServiceMock.Object);
		};

		Cleanup after = () =>
		{
			
		};
	}

	[Subject(typeof(DefaultRegistrationService), ".Register")]
	public class When_register_method_is_called : DefaultRegistrationServiceSpecs
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
			static Func<IQueryable<User>> _dummyUsersFn;
			Establish context = () =>
			{
				_fakeUsers = new[]{
					new User{
						EMail = "test@gmail.com",
						Id = 1,
						UserName = "chandu"
					}
				}.AsQueryable();	
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
				_userNameExistsException.ShouldBeAssignableTo<UsernameExistsException>();

			It should_report_the_duplicate_username = () =>
				(_userNameExistsException as UsernameExistsException).Values.ShouldContain(new[]{"chandu"});
		}

		public class when_registering_with_duplicate_EMail
		{
			static Exception _emailExistsException;
			static Func<IQueryable<User>> _dummyUsersFn;
			Establish context = () =>
			{
				_fakeUsers = new[]{
					new User{
						EMail = "test@gmail.com",
						Id = 1,
						UserName = "chandu"
					}
				}.AsQueryable();
			};

			Because of = () =>
			{
				_emailExistsException = Catch.Exception(() => _registrationService.Register(new User
				{
					UserName = "testchandu",
					EMail = "test@gmail.com"
				}, "somepassword"));
			};
			It should_throw_user_exists_exception = () =>
				_emailExistsException.ShouldBeAssignableTo<EMailExistsException>();

			It should_report_the_duplicate_email = () =>
				(_emailExistsException as EMailExistsException).Values.ShouldContain(new[] { "test@gmail.com" });
		}

		public class when_registering_with_valid_registration_information
		{
			static RegistrationResult _registrationResult;
			static FakeUser _user;

			Establish context = () =>
			{
				_fakeUsers = Enumerable.Empty<User>().AsQueryable(); 
				_user = new FakeUser
					{
						EMail = "test@gmail.com",
						Id = 1,
						UserName = "chandu"
					};
				_uowMock = new Mock<IUnitOfWork>();
				_uowMock
					.Setup(a => a.Attach(Moq.It.Is<User>(u => u.UserName == "chandu")))
					.Verifiable();

				_uowMock
					.Setup(a => a.Commit())
					.Verifiable();

				_mailServiceMock
					.Setup(a => a.SendMailAsync(Moq.It.IsAny<string>(), Moq.It.IsAny<string>(), Moq.It.IsAny<string>()))
					.Verifiable();

				_registrationService = new DefaultRegistrationService(_uowMock.Object, _gittuContext.Object, _mailServiceMock.Object);

			};

			Because of = () =>
				_registrationResult  = _registrationService.Register(_user, "password");
		
			It should_hash_the_password_for_storage = () =>
				_user.ThePassword.ShouldNotBeEmpty();
			
			It should_generate_salt_for_the_passord_hash = () =>
				_user.TheSalt.ShouldNotBeEmpty();

			It should_have_saved_user_to_datastore = () =>
				_uowMock.Verify();

			It should_register_the_user_successfully = () => 
				_registrationResult.IsSuccess.ShouldEqual(true);

			It should_send_registration_email_to_user = () =>
				_mailServiceMock.Verify();

			It should_set_user_status_as_not_verified = () =>
			_user.Status.ShouldEqual(UserStatus.NotVerified);

			It should_set_user_role_to_site_user = () =>
				_user.Role.ShouldEqual(UserRole.SiteUser);
			
		}
	}
}