using Nancy;
using Nancy.Conventions;

namespace Gittu.Web.Core
{
	public class GittuNancyBootstrapper:DefaultNancyBootstrapper
	{
		protected override void ConfigureConventions(NancyConventions nancyConventions)
		{
			base.ConfigureConventions(nancyConventions);
			nancyConventions.StaticContentsConventions.Add(
				StaticContentConventionBuilder.AddDirectory("public", @"public")
				);
			nancyConventions.StaticContentsConventions.Add(
				StaticContentConventionBuilder.AddDirectory("app", @"public/app")
				);
		}
	}
}