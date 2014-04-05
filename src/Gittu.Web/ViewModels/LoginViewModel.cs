using System.Collections.Generic;
using System.Linq;

namespace Gittu.Web.ViewModels
{
	public class LoginViewModel
	{
		public string UserName { get; set; }

		public string Password { get; set; }

		public bool RememberMe { get; set; }

	}
}