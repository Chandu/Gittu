using System;
using System.Linq;
using Gittu.Web.Domain;
using Gittu.Web.Domain.Entities;
using Gittu.Web.Exceptions;
using Gittu.Web.Security;

namespace Gittu.Web.Services
{
	public class DefaultRegistrationService : IRegistrationService
	{
		public IUnitOfWork UnitOfWork { get; set; }
		public IGittuContext GittuContext { get; set; }
		public IMailService MailService { get; set; }

		public DefaultRegistrationService(IUnitOfWork unitOfWork, IGittuContext gittuContext, IMailService mailService)
		{
			UnitOfWork = unitOfWork;
			GittuContext = gittuContext;
			MailService = mailService;
		}

		public RegistrationResult Register(User user, string password)
		{
			if(user == null)
			{
				throw new ArgumentException("User argument cannot be null", "user");
			}
			if(string.IsNullOrEmpty(password))
			{
				throw new ArgumentException("Password argument cannot be null", "password");
			}
			if(GittuContext.Users.Any(a => a.UserName == user.UserName))
			{
				throw new DuplicateUserExistsException();
			}
			var saltToUse = Hasher.GenerateSalt();
			user.Salt = saltToUse;
			user.Password = Hasher.Hash(password, saltToUse);
			UnitOfWork.Attach(user);
			MailService.SendMailAsync(user.EMail, "Thanks for signing up", "Some body");
			return new RegistrationResult
			{
				IsSuccess = true,
				Message = "User registered successfully"
			};
		}
	}
}