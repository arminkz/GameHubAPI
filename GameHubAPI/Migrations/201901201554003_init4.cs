namespace GameHubAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Description");
        }
    }
}
