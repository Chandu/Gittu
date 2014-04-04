using System.Collections.Generic;

namespace Gittu.Web.ViewModels
{
	public class LoginViewModel:IInvalidInput
	{
		public string UserName { get; set; }

		public string Password { get; set; }

		public bool RememberMe { get; set; }

		public IDictionary<string, IEnumerable<string>> Errors { get; set; }

	}
}