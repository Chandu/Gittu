using System;
using System.Collections.Generic;
using AutoMapper;
using Gittu.Web.Domain.Entities;
using Gittu.Web.Extensions;
using Gittu.Web.Services;
using Gittu.Web.ViewModels;
using Nancy;
using Nancy.Extensions;
using Nancy.ModelBinding;

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
				if (ModelValidationResult.IsValid)
				{
					try
					{
						var user = Mapper.Map<User>(registrationData);
						var registrationResult = registrationService.Register(user, registrationData.Password);
						if (registrationResult.IsSuccess)
						{
							var toReturn = Response.AsRedirect("/login");

							//I know, I know this looks stupid, but the Location header in a post reponse is eaten by the Browser monster and will automatically redirect. I want the location it in the jquery reponse header.
							toReturn.Headers.Remove("Location");
							toReturn.Headers.Add("X-REDIRECT", Context.ToFullPath("/login"));
							return toReturn;
						}
						return Response.AsJson(new InvalidInputResponse
						{
							Messages = new Dictionary<string, IEnumerable<string>>
							{
								{"", new[] {registrationResult.Message}}
							},
							Status = (int)HttpStatusCode.BadRequest
						}, HttpStatusCode.BadRequest);
					}
					catch (AggregateException ex)
					{
						throw;
					}
					catch (Exception ex)
					{
						return ex.AsJson(Response);
					}
				}
				return Response.AsJson(ModelValidationResult.ToInvalidInput(), HttpStatusCode.BadRequest);
			};
		}
	}
}