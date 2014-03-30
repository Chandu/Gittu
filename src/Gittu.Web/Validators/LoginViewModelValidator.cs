using FluentValidation;
using Gittu.Web.ViewModels;

namespace Gittu.Web.Validators
{
	public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
	{
		public LoginViewModelValidator()
		{
			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Password is required.")
				.Configure(p => p.CascadeMode = CascadeMode.StopOnFirstFailure)
				.Length(4, 50)
				.WithMessage("Password is invalid.")
				;

			RuleFor(x => x.UserName)
				.NotEmpty()
				.WithMessage("Username is required.")
				.Configure(p => p.CascadeMode = CascadeMode.StopOnFirstFailure)
				.Length(4, 50)
				.WithMessage("Username is invalid.")
				;

		}
	}
}