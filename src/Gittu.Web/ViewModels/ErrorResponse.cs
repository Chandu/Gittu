using Nancy;

namespace Gittu.Web.ViewModels
{
	public class ErrorResponse
	{
		public int Status { get; set; }

		public string Message { get; set; }

		public string Details { get; set; }
	}
}