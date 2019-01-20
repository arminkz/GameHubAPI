namespace GameHubAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "PicUrl", c => c.String());
            AddColumn("dbo.Games", "GameConfig", c => c.String());
            AddColumn("dbo.Games", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "AuthorId");
            AddForeignKey("dbo.Games", "AuthorId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "AuthorId", "dbo.Users");
            DropIndex("dbo.Games", new[] { "AuthorId" });
            DropColumn("dbo.Games", "AuthorId");
            DropColumn("dbo.Games", "GameConfig");
            DropColumn("dbo.Games", "PicUrl");
        }
    }
}
