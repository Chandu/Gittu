using System.Linq;
using Gittu.Web.ViewModels;
using Nancy;
using Nancy.Validation;

namespace Gittu.Web.Extensions
{
	public static class ModelValidatorResultExtensions
	{
		public static InvalidInputResponse ToInvalidInput(this ModelValidationResult modelValidationResult, int status = (int) HttpStatusCode.BadRequest)
		{
			return new InvalidInputResponse
					{
						Messages = modelValidationResult.Errors.SelectMany(a => a.Value.Select(b => new
						{
							PropertyName = a.Key,
							Message = b.ErrorMessage
						})).ToDictionary(a => a.PropertyName, a => a.Message),
						Status = status
					};
		}
	}
}