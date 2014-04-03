using System.Linq;
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
			switch (response.StatusCode)
			{
				case HttpStatusCode.BadRequest:
				{
					response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
					var result = response.Body.DeserializeJson<InvalidInputResponse>();
					result.Messages.ShouldNotBeEmpty();
					result.Messages.Values.SelectMany(a => a).ShouldContain(message);		
				}
					break;
				default:
				{
					response.StatusCode.ShouldEqual(HttpStatusCode.InternalServerError);
					var result = response.Body.DeserializeJson<ErrorResponse>();
					result.Message.ShouldNotBeEmpty();
					result.Message.ShouldEqual(message);		
				}
					break;
			}
		}
	}
}