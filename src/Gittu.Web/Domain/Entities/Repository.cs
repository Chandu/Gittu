using System;
namespace Gittu.Web.Domain.Entities
{
	public class Repository:IEntity
	{
		public virtual long Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string IconUrl { get; set; }
		public virtual string Description { get; set; }
		public virtual RepositoryType Type { get; set; }
		public virtual string Url { get; set; }
		public virtual DateTime LastUpdatedDate { get; set; }
		public virtual string LastUpdatedBy { get; set; }
		public bool IsNew()
		{
			return Id == 0;
		}
	}
}