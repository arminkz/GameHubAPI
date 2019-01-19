namespace GameHubAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        UserId1 = c.Int(nullable: false),
                        UserId2 = c.Int(nullable: false),
                        pending = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId1, t.UserId2 })
                .ForeignKey("dbo.Users", t => t.UserId1, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId2)
                .Index(t => t.UserId1)
                .Index(t => t.UserId2);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friendships", "UserId2", "dbo.Users");
            DropForeignKey("dbo.Friendships", "UserId1", "dbo.Users");
            DropIndex("dbo.Friendships", new[] { "UserId2" });
            DropIndex("dbo.Friendships", new[] { "UserId1" });
            DropTable("dbo.Friendships");
        }
    }
}
