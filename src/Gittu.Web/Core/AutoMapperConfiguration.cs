using System;
using System.Linq;
using AutoMapper;

namespace Gittu.Web.Core
{
	//http://stackoverflow.com/questions/11724326/automatic-discovery-of-automapper-configurations
	public static class AutoMapperConfiguration
	{
		public static void Configure()
		{
			Mapper.Initialize(x => GetConfiguration(Mapper.Configuration));
		}

		private static void GetConfiguration(IConfiguration configuration)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (var assembly in assemblies)
			{
				var profiles = assembly.GetTypes().Where(x => x != typeof(Profile) && typeof(Profile).IsAssignableFrom(x));
				foreach (var profile in profiles)
				{
					configuration.AddProfile((Profile)Activator.CreateInstance(profile));
				}
			}
		}
	}
}