namespace Gittu.Web.ViewModels
{
	public class RegisterViewModel
	{
		public string Email { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string ConfirmPassword { get; set; }

		public bool TermsAgreed { get; set; }
	}
}