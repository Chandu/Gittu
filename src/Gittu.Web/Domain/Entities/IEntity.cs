using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gittu.Web.Domain.Entities
{
	public interface IEntity
	{
		long Id { get; set; }
		bool IsNew();
	}
}