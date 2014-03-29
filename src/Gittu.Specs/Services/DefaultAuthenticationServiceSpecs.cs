using System;
using System.Linq;
using CsQuery.Engine.PseudoClassSelectors;
using Gittu.Specs.Fakes;
using Gittu.Web.Domain;
using Gittu.Web.Domain.Entities;
using Gittu.Web.Security;
using Gittu.Web.Services;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Gittu.Specs.Services
{
	[Subject("Default Authentication service")]
	public class DefaultAuthenticationServiceSpecs
	{
		static DefaultAuthenticationService _authenticationService;
		static Mock<IGittuContext> _gittuContextMock;
		static IQueryable<User> _fakeUsers = Enumerable.Empty<User>().AsQueryable();
		static Mock<IHasher> _hasherMock = new Mock<IHasher>();
		static Exception _exception;
		protected static LoginResult _loginResult;

		Establish context = () =>
		{
			_gittuContextMock = new Mock<IGittuContext>();
			_gittuContextMock
				.Setup(a => a.Users)
				.Returns(() => _fakeUsers);
			_authenticationService = new DefaultAuthenticationService(_gittuContextMock.Object, _hasherMock.Object);
		};
		
		[Subject(typeof(DefaultAuthenticationService), ".Validate")]
		public class with_empty_username:DefaultAuthenticationServiceSpecs
		{
			private Because of = () =>
				_loginResult = _authenticationService.Validate(string.Empty, "somepassword");

			private Behaves_like<InvalidUsernamePasswordBehaviours> _an_invalid_result;
		}

		public class with_empty_password : DefaultAuthenticationServiceSpecs
		{
			private Because of = () =>
				_loginResult = _authenticationService.Validate("someuser", string.Empty);

			private Behaves_like<InvalidUsernamePasswordBehaviours> _an_invalid_result;
		}

		public class with_invalid_username_password_combination : DefaultAuthenticationServiceSpecs
		{
			private Establish context = () =>
			{
				_fakeUsers = new[]
				{
					new FakeUser
					{
						EMail = "some@gmail.com",
						UserName = "some-username"
					}
				}.AsQueryable();
			};

			private Because of = () =>
				_loginResult = _authenticationService.Validate("some-missing-user", "some-password");

			private Behaves_like<InvalidUsernamePasswordBehaviours> _an_invalid_result;

			private It should_not_be_able_to_find_user = () =>
				_loginResult.WasUserFound.ShouldBeFalse();
		}

		public class with_valid_username_invalid_password_combination:DefaultAuthenticationServiceSpecs
		{
			private Establish context = () =>
			{
				_hasherMock
					.Setup(a => a.Hash(Moq.It.IsAny<string>(), Moq.It.IsAny<byte[]>()))
					.Returns(HashUtils.GenerateSalt());

				_fakeUsers = new[]
				{
					new FakeUser
					{
						EMail = "some@gmail.com",
						UserName = "some-username",
						ThePassword = HashUtils.GenerateSalt(),
						TheSalt = HashUtils.GenerateSalt()
					}
				}.AsQueryable();
			};
			private Because of = () =>
				_loginResult = _authenticationService.Validate("some-username", "somepassword");

			private Behaves_like<InvalidUsernamePasswordBehaviours> _an_invalid_result;

			private It should_be_able_to_find_user = () =>
				_loginResult.WasUserFound.ShouldBeTrue();
		}

		public class with_valid_username_and_password_combination : DefaultAuthenticationServiceSpecs
		{
			private Establish context = () =>
			{
				var pwdBytes = HashUtils.GenerateSalt();
				_hasherMock
					.Setup(a => a.Hash(Moq.It.IsAny<string>(), Moq.It.IsAny<byte[]>()))
					.Returns(pwdBytes);

				_fakeUsers = new[]
				{
					new FakeUser
					{
						EMail = "some@gmail.com",
						UserName = "some-username",
						ThePassword = pwdBytes ,
						TheSalt = HashUtils.GenerateSalt() 
					}
				}.AsQueryable();
			};

			private Because of = () =>
				_loginResult = _authenticationService.Validate("some-username", "somepassword");

			It should_return_successful_result = () =>
			 _loginResult.IsSuccess.ShouldBeTrue();

			It should_return_ValidatedSuccesfully_message = () =>
				_loginResult.Message.ShouldEqual("Validated successfully.");

			private It should_be_able_to_find_user = () =>
				_loginResult.WasUserFound.ShouldBeTrue();
		}

		

		[Behaviors]
		class InvalidUsernamePasswordBehaviours
		{
			protected static LoginResult _loginResult;

			It should_return_unsuccessful_result = () =>
			 _loginResult.IsSuccess.ShouldBeFalse();

			It should_return_InvalidUserNamePassword_message = () =>
				_loginResult.Message.ShouldEqual("Invalid username/password.");
		}

		
	}
}