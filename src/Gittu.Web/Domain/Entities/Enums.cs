namespace Gittu.Web.Domain.Entities
{
	public enum RepositoryType
	{
		Public,
		Private
	}

	public enum UserStatus
	{
		Unknown,
		NotVerified,
		Active,
		Inactive
	}

	public enum UserRole
	{
		Guest,
		SiteUser,
		SiteAdmin,
		TheGod
	}
}