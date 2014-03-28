using System.Threading.Tasks;

namespace Gittu.Web.Services
{
	public interface IMailService
	{
		Task SendMailAsync(string address, string subject, string body);
	}
}