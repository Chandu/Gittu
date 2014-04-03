using System.Collections.Generic;
using System.Linq;
using Gittu.Web.Domain.Entities;

namespace Gittu.Web.Exceptions
{
	public class EMailExistsException:DuplicateEntryException, IUserException
	{
		public EMailExistsException(string email):base("EMail", email, typeof(User))
		{
			
		}


		public override IDictionary<string, IEnumerable<string>> Messages
		{
			get
			{
				return new Dictionary<string, IEnumerable<string>>
				{
					{"EMail", new[]
				{
					string.Format("A user with the email {0} already exists in the system.", Values.FirstOrDefault())
				}}
				};
			}
		}
	}
}