using System.Collections.Generic;
namespace Gittu.Web.ViewModels
{
	public class InvalidInputViewModel
	{
		public int Status { get; set; }
		public IDictionary<string, string> Messages { get; set; }
	}
}