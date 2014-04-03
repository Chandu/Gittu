using FluentMigrator;

namespace Gittu.Migrations
{
	[Migration(201404022013)]
	public class CreateUserTable : Migration
	{
		public override void Down()
		{
			Delete.Table("Users");
		}

		public override void Up()
		{
			Create.Table("Users")
				.WithColumn("Id").AsInt64().PrimaryKey().Identity()
				.WithColumn("Username").AsString(255).NotNullable().Indexed()
				.WithColumn("EMail").AsString(255).NotNullable().Indexed()
				.WithColumn("Password").AsBinary().NotNullable()
				.WithColumn("Salt").AsBinary().NotNullable()
				;
		}
	}
}