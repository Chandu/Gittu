using System.Linq;
using Gittu.Web.ViewModels;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace Gittu.Specs
{
	internal static class BrowserResponseExtensions
	{
		public static void ShouldHaveErroredWith<T>(this BrowserResponse response, string message)
			where T:IInvalidInput
		{
			response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
			var result = response.GetModel<T>();
			result.Errors.ShouldNotBeEmpty();
			result.Errors.Values.SelectMany(a => a).ShouldContain(message);
		}
	}
}