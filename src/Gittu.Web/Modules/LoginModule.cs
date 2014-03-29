using System.Collections.Generic;
using Gittu.Web.Extensions;
using Gittu.Web.Services;
using Gittu.Web.ViewModels;
using Nancy;
using Nancy.ModelBinding;

namespace Gittu.Web.Modules
{
	public class LoginModule : NancyModule
	{
		public IAuthenticationService AuthenticationService { get; set; }

		public LoginModule(IAuthenticationService authenticationService)
			: base("login")
		{
			AuthenticationService = authenticationService;
			Post["/"] = _ =>
			{
				var loginViewModel = this.BindAndValidate<LoginViewModel>();
				if (!ModelValidationResult.IsValid)
				{
					return Response.AsJson(ModelValidationResult.ToInvalidInput(), HttpStatusCode.BadRequest);
				}
				var loginResult = 
					AuthenticationService.Validate(loginViewModel.UserName, loginViewModel.Password);
				if(loginResult.IsSuccess)
				{
					return Response.AsRedirect("/");	
				}
				return Response.AsJson(new InvalidInputResponse
				{
					Messages = new Dictionary<string, string>
						{
							{"", loginResult.Message}
						},
					Status = (int)HttpStatusCode.Unauthorized
				}, HttpStatusCode.Unauthorized);
			};
		}
	}
}