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
		public IAuthenticationService AuthenticationService { get; set; }

		public LoginModule(IAuthenticationService authenticationService)
			: base("account")
		{
			AuthenticationService = authenticationService;
			Post["login"] = _ =>
			{
				var loginViewModel = this.BindAndValidate<LoginViewModel>();
				if (!ModelValidationResult.IsValid)
				{
					loginViewModel.Errors = ModelValidationResult.ToDictionary();
					return View["Login", loginViewModel].WithStatusCode(HttpStatusCode.BadRequest);
				}

				try
				{
					var loginResult =
					AuthenticationService.Validate(loginViewModel.UserName, loginViewModel.Password);
					if (loginResult.IsSuccess)
					{
						var userGuid = Guid.NewGuid();
						AuthenticationService.SaveUserToken(loginViewModel.UserName, userGuid);
						return this.LoginAndRedirect(userGuid, loginViewModel.RememberMe ? DateTime.Now.AddDays(15) : new DateTime?());
					}
					loginViewModel.Errors = new Dictionary<string, IEnumerable<string>>
						{
							{"", new[] {loginResult.Message}}
						};
				}
				catch (AggregateException ex)
				{
					throw;
				}
				catch (Exception ex)
				{
					if (!(ex is IUserException))
					{
						throw;
					}
					loginViewModel.Errors = (ex as IUserException).Errors;
				}

				return View["Login", loginViewModel].WithStatusCode(HttpStatusCode.Unauthorized);
			};
		}
	}
}