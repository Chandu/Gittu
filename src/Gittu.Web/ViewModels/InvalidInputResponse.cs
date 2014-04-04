using System.Collections.Generic;

namespace Gittu.Web.ViewModels
{
	public class InvalidInputResponse : IInvalidInput
	{
		public int Status { get; set; }

		public IDictionary<string, IEnumerable<string>> Errors { get; set; }
	}
}