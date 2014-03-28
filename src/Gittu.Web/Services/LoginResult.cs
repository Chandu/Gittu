using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Services
{
	public class LoginResult
	{
		public LoginResult(bool isSuccess, string message)
		{
			IsSuccess = isSuccess;
			Message = message;
		}

		internal LoginResult()
		{
		}

		public bool IsSuccess { get; internal set; }

		public string Message { get; internal set; }

		public User User { get; set; }
	}
}