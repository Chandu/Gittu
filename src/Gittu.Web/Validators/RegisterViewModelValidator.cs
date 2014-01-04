using FluentValidation;
using Gittu.Web.ViewModels;

namespace Gittu.Web.Validators
{
	public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
	{
		public RegisterViewModelValidator()
		{
			RuleFor(x => x.Email).NotEmpty()
				.WithMessage("Email address is required.")
				.EmailAddress()
				.WithMessage("Email address is invalid.")
			;

			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Password is required.")
				.Length(4, 16)
				.WithMessage("Password is invalid.")
				;

			RuleFor(x => x.UserName)
				.NotEmpty()
				.WithMessage("Username is required.")
				.Length(4, 16)
				.WithMessage("Username is invalid.")
				;

			RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.Password)
				.WithMessage("Password and Confirm Password donot match.")
				;
		}
	}
}