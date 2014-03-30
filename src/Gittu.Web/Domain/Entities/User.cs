using System.Linq;
using Gittu.Web.Security;

namespace Gittu.Web.Domain.Entities
{
	public class User:IEntity
	{
		public virtual long Id { get; set; }

		public virtual string UserName { get; set; }

		public virtual string EMail { get; set; }

		//Refer to the URL: http://blog.oneunicorn.com/2012/03/26/code-first-data-annotations-on-non-public-properties/ for mapping private/protected properties in EF Code First later.
		protected internal virtual byte[] Password { get; set; }

		protected internal virtual byte[] Salt { get; set; }

		public void SetPassword(byte[] password)
		{
			Password = password;
		}

		public void SetSalt(byte[] salt)
		{
			Salt = salt;
		}

		public bool ValidatePassword(string password, IHasher hasher)
		{
			var hashedPassword = hasher.Hash(password, Salt);
			return hashedPassword.SequenceEqual(Password);
		}

		public bool IsNew()
		{
			return Id == 0;
		}
	}
}