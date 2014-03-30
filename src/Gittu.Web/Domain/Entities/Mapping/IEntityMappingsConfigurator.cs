using System.Data.Entity;

namespace Gittu.Web.Domain.Entities.Mapping
{
	public interface IEntityMappingsConfigurator
	{
		void Configure(DbModelBuilder modelBuilder);
	}
}
