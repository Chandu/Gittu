using System.Collections.Generic;
using Gittu.Web.Extensions;
using Gittu.Web.ViewModels;
using Nancy;
using Nancy.ModelBinding;
using System.Linq;
using Gittu.Web.Services;
using AutoMapper;
using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Modules
{
	public class RegisterModule : NancyModule
	{
		public RegisterModule(IRegistrationService registrationService)
			: base("/register")
		{
			Get["/"] = parameters => View["Register.html"];

			Post["/"] = parameters =>
			{
				var registrationData = this.BindAndValidate<RegisterViewModel>();
				if(ModelValidationResult.IsValid)
				{
					var user = Mapper.Map<User>(registrationData);
					var registrationResult  = registrationService.Register(user, registrationData.Password);
					if (registrationResult.Item1)
					{
						return Response.AsRedirect("login");	
					}
					return Response.AsJson(new InvalidInputResponse
					{
						Messages = new Dictionary<string, string>
						{
							{"", registrationResult.Item2}
						},
						Status = (int)HttpStatusCode.BadRequest 
					}, HttpStatusCode.BadRequest);
				}
				return Response.AsJson(ModelValidationResult.ToInvalidInput(), HttpStatusCode.BadRequest);
			};
		}
	}
}