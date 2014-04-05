using System;
using System.Collections.Generic;
using Gittu.Web.Exceptions;
using Gittu.Web.Services;
using Gittu.Web.ViewModels;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.ModelBinding;

namespace Gittu.Web.Modules
{
	public class LoginModule : NancyModule
	{
		private readonly IUserTokenStore _userTokenStore;
		private readonly IAuthenticationService _authenticationService;

		public LoginModule(IAuthenticationService authenticationService)
			: this(authenticationService, new InMemoryUserTokenStore())
		{
		}

		public LoginModule(IAuthenticationService authenticationService, IUserTokenStore userTokenStore)
			: base("account")
		{
			_userTokenStore = userTokenStore;
			_authenticationService = authenticationService;

			Post["login"] = _ =>
			{
				var loginViewModel = this.BindAndValidate<LoginViewModel>();
				if (!ModelValidationResult.IsValid)
				{
					ViewBag._Errors_ = ModelValidationResult.ToDictionary();
					return View["Login", loginViewModel].WithStatusCode(HttpStatusCode.BadRequest);
				}

				try
				{
					var loginResult =
					_authenticationService.Validate(loginViewModel.UserName, loginViewModel.Password);
					if (loginResult.IsSuccess)
					{
						var userGuid = Guid.NewGuid();
						_userTokenStore.Save(loginViewModel.UserName, userGuid);
						return this.LoginAndRedirect(userGuid, loginViewModel.RememberMe ? DateTime.Now.AddDays(15) : new DateTime?());
					}
					ViewBag._Errors_ = new Dictionary<string, IEnumerable<string>>
						{
							{"", new[] {loginResult.Message}}
						};
				}
				catch (AggregateException)
				{
					throw;
				}
				catch (Exception ex)
				{
					if (!(ex is IUserException))
					{
						throw;
					}
					ViewBag._Errors_ = (ex as IUserException).Errors;
				}

				return View["Login", loginViewModel].WithStatusCode(HttpStatusCode.BadRequest);
			};
		}
	}
}