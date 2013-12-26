using FluentValidation;
using Gittu.Web.ViewModels;

namespace Gittu.Web.Validators
{
	public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
	{
		public RegisterViewModelValidator()
		{
			RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.Password)
				.WithMessage("Password and Confirm Password donot match.")
				;
		}
	}
}