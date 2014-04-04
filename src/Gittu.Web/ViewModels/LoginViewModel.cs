using System.Collections.Generic;
using System.Linq;

namespace Gittu.Web.ViewModels
{
	public class LoginViewModel : IInvalidInput
	{
		public string UserName { get; set; }

		public string Password { get; set; }

		public bool RememberMe { get; set; }

		//TODO: (CV) These should go to ViewBag later
		public IDictionary<string, IEnumerable<string>> Errors { get; set; }

		public IEnumerable<string> FlattenedErrors
		{
			get { return (Errors != null)? Errors.SelectMany(a => a.Value):Enumerable.Empty<string>(); }
		}

		public bool HasErrors
		{
			get { return FlattenedErrors.Any(); }
		}
	}
}