using System;

namespace Gittu.Web.Domain.Entities
{
	public class UserToken
	{
		public Guid Token { get; set; }
		public string UserName { get; set; }
	}
}