using System.Collections.Generic;

namespace Gittu.Web.ViewModels
{
	public class RegisterViewModel:IInvalidInput
	{
		public string Email { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public bool TermsAgreed { get; set; }

		public IDictionary<string, IEnumerable<string>> Errors { get; set; }
	}
}