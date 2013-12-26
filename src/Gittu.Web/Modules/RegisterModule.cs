using Gittu.Web.Extensions;
using Gittu.Web.ViewModels;
using Nancy;
using Nancy.ModelBinding;
using System.Linq;

namespace Gittu.Web.Modules
{
	public class RegisterModule : NancyModule
	{
		public RegisterModule()
			: base("/register")
		{
			Get["/"] = parameters => View["Register.html"];

			Post["/"] = parameters =>
			{
				this.BindAndValidate<RegisterViewModel>();
				if(ModelValidationResult.IsValid)
				{
					return HttpStatusCode.OK;
				}
				return Response.AsJson(ModelValidationResult.ToInvalidInput(), HttpStatusCode.BadRequest);
			};
		}
	}
}