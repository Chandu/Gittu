using System.Linq;
using Gittu.Web.ViewModels;
using Nancy;
using Nancy.Validation;

namespace Gittu.Web.Extensions
{
	public static class ModelValidatorResultExtensions
	{
		public static InvalidInputViewModel ToInvalidInput(this ModelValidationResult modelValidationResult, int status = (int) HttpStatusCode.BadRequest)
		{
			return new InvalidInputViewModel
					{
						Messages = modelValidationResult.Errors.SelectMany(a => a.MemberNames.Select(b => new
						{
							PropertyName = b,
							Message = a.GetMessage(b)
						})).ToDictionary(a => a.PropertyName, a => a.Message),
						Status = status
					};
		}
	}
}