using AutoMapper;
using Gittu.Web.Domain.Entities;
using Gittu.Web.ViewModels;

namespace Gittu.Web.Mapping
{
	public class RegisterViewModelProfile : Profile
	{
		public override string ProfileName
		{
			get
			{
				return "RegisterViewModelProfile";
			}
		}

		protected override void Configure()
		{
			CreateMap<RegisterViewModel, User>();
		}
	}
}