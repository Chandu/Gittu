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
			: base("/account")
		{
			Post["/register"] = _ =>
			{
				var registrationData = this.BindAndValidate<RegisterViewModel>();
				if(ModelValidationResult.IsValid)
				{
					var user = Mapper.Map<User>(registrationData);
					var registrationResult  = registrationService.Register(user, registrationData.Password);
					if (registrationResult.IsSuccess)
					{
						return Response.AsRedirect("login");	
					}
					return Response.AsJson(new InvalidInputResponse
					{
						Messages = new Dictionary<string, IEnumerable<string>>
						{
							{"", new [] {registrationResult.Message}}
						},
						Status = (int)HttpStatusCode.BadRequest 
					}, HttpStatusCode.BadRequest);
				}
				return Response.AsJson(ModelValidationResult.ToInvalidInput(), HttpStatusCode.BadRequest);
			};
		}
	}
}