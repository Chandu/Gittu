using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Gittu.Web.ViewModels;
using Nancy;
using Nancy.Validation;

namespace Gittu.Web
{
	public static class DictionaryExtensions
	{
		public static InvalidInputResponse ToInvalidInput(this ModelValidationResult modelValidationResult, int status = (int) HttpStatusCode.BadRequest)
		{
			return new InvalidInputResponse
					{
						Errors = modelValidationResult.Errors.SelectMany(a => a.Value.Select(b => new
						{
							PropertyName = a.Key,
							Message = b.ErrorMessage
						}))
						.GroupBy(a => a.PropertyName)
						.ToDictionary(a => a.Key, a => a.Select(b => b.Message)),
						Status = status
					};
		}

		public static IDictionary<string, IEnumerable<string>> ToDictionary(this ModelValidationResult modelValidationResult)
		{
			return modelValidationResult
				.Errors
				.ToDictionary(a => a.Key, a => a.Value.Select(b => b.ErrorMessage));
		}

		public static NameValueCollection ToNameValueCollection(
		this IDictionary<string, IEnumerable<string>> dict)
		{
			var nameValueCollection = new NameValueCollection();

			foreach (var kvp in dict.Where(kvp => kvp.Value != null))
			{
				kvp.Value.ToList().ForEach(a => nameValueCollection.Add(kvp.Key, a));
			}

			return nameValueCollection;
		}
	}
}