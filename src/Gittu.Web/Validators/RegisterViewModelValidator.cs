﻿using FluentValidation;
using Gittu.Web.ViewModels;

namespace Gittu.Web.Validators
{
	public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
	{
		public RegisterViewModelValidator()
		{
			RuleFor(x => x.EMail).NotEmpty()
				.WithMessage("EMail address is required.")
				.Configure(p => p.CascadeMode = CascadeMode.StopOnFirstFailure)
				.EmailAddress()
				.WithMessage("EMail address is invalid.")
			;

			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Password is required.")
				.Configure(p => p.CascadeMode = CascadeMode.StopOnFirstFailure)
				.Length(4, 16)
				.WithMessage("Password is invalid.")
				;

			RuleFor(x => x.UserName)
				.NotEmpty()
				.WithMessage("Username is required.")
				.Configure(p => p.CascadeMode = CascadeMode.StopOnFirstFailure)
				.Length(4, 16)
				.WithMessage("Username is invalid.")
				;

			RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.Password)
				.WithMessage("Password and Confirm Password donot match.")
				;

			RuleFor(x => x.TermsAgreed)
				.Equal(true)
				.WithMessage("Please read and Agree to our Terms & Conditions to complete the registration.");
		}
	}
}