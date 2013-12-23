using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Gittu.Web.Domain.Entities;
using Gittu.Web.ViewModels;

namespace Gittu.Web.Mapping
{
	public class RepositoryProfile:Profile
	{
		public override string ProfileName
		{
			get
			{
				return "RepositoryProfile";
			}
		}

		protected override void Configure()
		{
			CreateMap<Repository, RepositoryViewModel>();
		} 
	}
}