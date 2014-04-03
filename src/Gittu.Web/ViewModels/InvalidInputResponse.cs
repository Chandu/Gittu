using System.Collections.Generic;
using Nancy.Responses;

namespace Gittu.Web.ViewModels
{
	public class InvalidInputResponse
	{
		public int Status { get; set; }
		public IDictionary<string, IEnumerable<string>> Messages { get; set; }
	}
}