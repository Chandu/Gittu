using FluentMigrator;

namespace Gittu.Migrations
{
	[Migration(201404042130)]
	public class AddUserStatusAndRoleToUser:Migration
	{
		public override void Up()
		{
			Alter.Table("Users").AddColumn("Status").AsInt32().WithDefaultValue(0);
			Alter.Table("Users").AddColumn("Role").AsInt32().WithDefaultValue(0);
		}

		public override void Down()
		{
			Delete.Column("Role").Column("Status").FromTable("Users");
		}
	}
}