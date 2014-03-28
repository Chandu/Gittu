using Gittu.Web.ViewModels;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;

namespace Gittu.Specs
{
	internal static class BrowserResponseExtensions
	{
		public static void ShouldHaveErroredWith(this BrowserResponse response, string message)
		{
			response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
			var result = response.Body.DeserializeJson<InvalidInputResponse>();
			result.Messages.ShouldNotBeEmpty();
			result.Messages.Values.ShouldContain(message);
		}
	}
}