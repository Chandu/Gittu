using Gittu.Web.Domain.Entities;

namespace Gittu.Specs.Fakes
{
	internal class FakeUser : User
	{
		public byte[] ThePassword
		{
			get { return base.Password; }
			set { base.Password = value; }
		}

		public byte[] TheSalt
		{
			get { return base.Salt; }
			set { base.Salt = value; }
		}
	}
}