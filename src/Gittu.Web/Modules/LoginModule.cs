using System.Collections.Generic;
using Gittu.Web.Extensions;
using Gittu.Web.Services;
using Gittu.Web.ViewModels;
using Nancy;
using Nancy.Extensions;
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
			Post["/login"] = _ =>
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
					var toReturn = Response.AsRedirect("/");
					//I know, I know this looks stupid, but the Location header in a post reponse is eaten by the Browser monster and will automatically redirect. I want the location it in the jquery reponse header.
					toReturn.Headers.Remove("Location");
					toReturn.Headers.Add("X-REDIRECT", Context.ToFullPath("/"));
					return toReturn;	
				}
				return Response.AsJson(new InvalidInputResponse
				{
					Messages = new Dictionary<string, IEnumerable<string>>
						{
							{"", new [] {loginResult.Message}}
						},
					Status = (int)HttpStatusCode.Unauthorized
				}, HttpStatusCode.Unauthorized);
			};
		}
	}
}