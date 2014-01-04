using System.Collections.Generic;
namespace Gittu.Web.ViewModels
{
	public class InvalidInputResponse
	{
		public int Status { get; set; }
		public IDictionary<string, string> Messages { get; set; }
	}
}