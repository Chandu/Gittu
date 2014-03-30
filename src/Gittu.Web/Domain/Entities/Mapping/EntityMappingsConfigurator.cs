using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Gittu.Web.Domain.Entities.Mapping
{
	public class EntityMappingsConfigurator : IEntityMappingsConfigurator
	{
		public void Configure(DbModelBuilder modelBuilder)
		{
			foreach (var mapper in GetEntityConfigurationInstances())
			{
				mapper.Map(modelBuilder);
			}
			
		}

		public static IEnumerable<IEntityMapper> GetEntityConfigurationInstances()
		{
			var type = typeof(IEntityMapper);
			return Assembly.GetExecutingAssembly()
					.GetTypes()
					.Where(p => type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
					.Select(a => Activator.CreateInstance(a) as IEntityMapper);
		}
	}
}