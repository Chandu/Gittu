namespace Gittu.Web.Domain.Entities
{
	public interface IEntity
	{
		long Id { get; set; }

		bool IsNew();
	}
}