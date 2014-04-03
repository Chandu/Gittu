using System.Collections.Generic;

namespace Gittu.Web.Exceptions
{
	public interface IUserException
	{
		IDictionary<string, IEnumerable<string>> Messages { get; }
	}
}