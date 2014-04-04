using System.Collections.Generic;

namespace Gittu.Web.ViewModels
{
	public interface IInvalidInput
	{
		IDictionary<string, IEnumerable<string>> Errors { get; }
	}
}