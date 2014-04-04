using System;
using System.Collections.Generic;
using AutoMapper;
using Gittu.Web.Domain.Entities;
using Gittu.Web.Exceptions;
using Gittu.Web.Extensions;
using Gittu.Web.Services;
using Gittu.Web.ViewModels;
using Nancy;
using Nancy.ModelBinding;

namespace Gittu.Web.Modules
{
	public class RegisterModule : AccountModule
	{
		public RegisterModule(IRegistrationService registrationService)
		{
			Post["register"] = _ =>
			{
				var registrationViewModel = this.BindAndValidate<RegisterViewModel>();
				if (!ModelValidationResult.IsValid)
				{
					registrationViewModel.Errors = ModelValidationResult.ToDictionary();
					return View["Register", registrationViewModel].WithStatusCode(HttpStatusCode.BadRequest);
				}
				try
				{
					var user = Mapper.Map<User>(registrationViewModel);
					var registrationResult = registrationService.Register(user, registrationViewModel.Password);
					if (registrationResult.IsSuccess)
					{
						return Response.AsRedirect("login");
					}
					registrationViewModel.Errors = new Dictionary<string, IEnumerable<string>>
					{
						{"", new[] {registrationResult.Message}}
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
					registrationViewModel.Errors = (ex as IUserException).Errors;
				}
				return View["Register",registrationViewModel].WithStatusCode(HttpStatusCode.BadRequest);
			};
		}
	}
}