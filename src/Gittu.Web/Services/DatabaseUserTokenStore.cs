using System;
using System.Data.Entity;
using System.Linq;

namespace Gittu.Web.Services
{
	public class DatabaseUserTokenStore : DbContext, IUserTokenStore
	{
		static DatabaseUserTokenStore()
		{
			Database.SetInitializer<DatabaseUserTokenStore>(null);
		}

		public DatabaseUserTokenStore()
			: this("GittuDB")
		{
		}

		public DatabaseUserTokenStore(string connectionName)
			: base(connectionName)
		{
		}

		private DbSet<UserToken> Tokens
		{
			get { return Set<UserToken>(); }
		}

		public void Save(string userName, Guid token)
		{
			var userToken = Tokens.FirstOrDefault(a => a.UserName == userName);
			if (userToken == null)
			{
				userToken = new UserToken
				{
					UserName = userName
				};
				Tokens.Add(userToken);
			}
			userToken.Token = token;
			SaveChanges();
		}

		public string Get(Guid token)
		{
			return Tokens
				.Where(a => a.Token == token)
				.Select(a => a.UserName)
				.FirstOrDefault();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder
				.Entity<UserToken>()
				.HasKey(a => a.UserName);
		}
	}

	internal class UserToken
	{
		public Guid Token { get; set; }

		public string UserName { get; set; }
	}
}