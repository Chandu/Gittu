namespace Gittu.Web.Services
{
	public class RegistrationResult
	{
		public RegistrationResult(bool isSuccess, string message)
		{
			IsSuccess = isSuccess;
			Message = message;
		}

		internal RegistrationResult()
		{
			
		}

		public bool IsSuccess { get; internal set; }

		public string Message { get; internal set; }
	}
}