using System.ComponentModel.DataAnnotations;

namespace Gittu.Web.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[EmailAddress]
		[MinLength(4)]
		public string Email { get; set; }

		[Required]
		[MinLength(4)]
		public string UserName { get; set; }

		[Required]
		[MinLength(6)]
		public string Password { get; set; }

		[Required]
		[MinLength(6)]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }
	}
}