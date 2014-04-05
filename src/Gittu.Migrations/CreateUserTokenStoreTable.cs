using FluentMigrator;
namespace Gittu.Migrations
{
	[Migration(201404051403)]
	public class CreateUserTokenStoreTable:Migration
	{
		public override void Down()
		{
			Delete.Table("UserTokens");
		}

		public override void Up()
		{
			Create.Table("UserTokens")
				.WithColumn("UserName").AsString(50).PrimaryKey()
				.WithColumn("Token").AsGuid().NotNullable()
				;
		}
	}
}