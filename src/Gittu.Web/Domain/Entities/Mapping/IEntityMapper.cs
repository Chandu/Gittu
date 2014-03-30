using System.Data.Entity;

namespace Gittu.Web.Domain.Entities.Mapping
{
	public interface IEntityMapper
	{
		void Map(DbModelBuilder modelBuilder);
	}
}
