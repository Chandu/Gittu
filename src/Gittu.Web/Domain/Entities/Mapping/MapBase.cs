using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Gittu.Web.Domain.Entities.Mapping
{
	public abstract class MapBase<T> : EntityTypeConfiguration<T>, IEntityMapper where T:class
	{
		public virtual void Map(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(this);
		}
		
	}
}