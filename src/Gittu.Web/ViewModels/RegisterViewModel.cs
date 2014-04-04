using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gittu.Web.ViewModels
{
	public class RegisterViewModel : IInvalidInput
	{
		public string EMail { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public bool TermsAgreed { get; set; }

		//TODO: (CV) These should go to ViewBag later
		public IDictionary<string, IEnumerable<string>> Errors { get; set; }

		public ICollection<string> FlattenedErrors
		{
			get { return new Collection<string>(((Errors != null) ? Errors.SelectMany(a => a.Value) : Enumerable.Empty<string>()).ToList()); }
		}
	}
}