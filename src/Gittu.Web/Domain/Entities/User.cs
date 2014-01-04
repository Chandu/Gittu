namespace Gittu.Web.Domain.Entities
{
	public class User
	{
		public virtual long Id { get; set; }

		public virtual string UserName { get; set; }

		public virtual string EMail { get; set; }

		protected internal virtual byte[] Password { get; set; }

		protected internal virtual byte[] Salt { get; set; }
	}
}