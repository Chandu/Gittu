using System.Collections.Generic;
using Machine.Specifications;
using Nancy;
using Nancy.Testing;
using System.Linq;

namespace Gittu.Specs
{
	internal static class BrowserResponseExtensions
	{
		public static void ShouldHaveErroredWith(this BrowserResponse response, string message)
		{
			response.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
			IDictionary<string, IEnumerable<string>> result = response.Context.ViewBag._Errors_ ;
			result.ShouldNotBeNull();
			result.ShouldNotBeEmpty();
			result.SelectMany(a => a.Value).ShouldContain(message);
		}
	}
}