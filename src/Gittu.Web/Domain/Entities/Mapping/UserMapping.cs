namespace Gittu.Web.Domain.Entities.Mapping
{
	public class UserMapping:MapBase<User>
	{
		public UserMapping()
		{
			HasKey(a => a.Id);
			Property(a => a.EMail)
				.HasMaxLength(255)
				.IsUnicode()
				.IsRequired();

			Property(a => a.UserName)
				.HasMaxLength(255)
				.IsUnicode()
				.IsRequired();

			Property(a => a.Password);
			Property(a => a.Salt);
			Property(a => a.Status);
			Property(a => a.Role);
		}
	}
}