namespace GameHubAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ll1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastLogin", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastLogin");
        }
    }
}
