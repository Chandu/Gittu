using System.Collections.Generic;
using System.Linq;
using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Exceptions
{
	public class UsernameExistsException : DuplicateEntryException, IUserException
	{
		public UsernameExistsException(string userName)
			: base("Username", userName, typeof(User))
		{
		}

		public override IDictionary<string, IEnumerable<string>> Messages
		{
			get
			{
				return new Dictionary<string, IEnumerable<string>>
				{
					{"Username", new[]
				{
					string.Format("A user with the username {0} already exists in the system.", Values.FirstOrDefault())
				}}
				};
			}
		}
	}
}