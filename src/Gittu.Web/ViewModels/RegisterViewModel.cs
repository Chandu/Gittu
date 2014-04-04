using System.Collections.Generic;
using System.Linq;

namespace Gittu.Web.ViewModels
{
	public class RegisterViewModel : IInvalidInput
	{
		public RegisterViewModel()
		{
			Errors = new Dictionary<string, IEnumerable<string>>();
		}
		public string EMail { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public bool TermsAgreed { get; set; }

		//TODO: (CV) These should go to ViewBag later
		public IDictionary<string, IEnumerable<string>> Errors { get; set; }

		public IEnumerable<string> FlattenedErrors
		{
			get { return (Errors != null) ? Errors.SelectMany(a => a.Value) : Enumerable.Empty<string>(); }
		}
	}
}