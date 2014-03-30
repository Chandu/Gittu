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
						}))
						.GroupBy(a => a.PropertyName)
						.ToDictionary(a => a.Key, a => a.Select(b => b.Message)),
						Status = status
					};
		}
	}
}