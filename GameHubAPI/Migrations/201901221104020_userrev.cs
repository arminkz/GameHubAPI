namespace GameHubAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userrev : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friendships", "UserId1", "dbo.Users");
            DropForeignKey("dbo.Games", "AuthorId", "dbo.Users");
            CreateTable(
                "dbo.UserReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Comment = c.String(),
                        Rating = c.Int(nullable: false),
                        Pending = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.AuthorId)
                .Index(t => t.UserId);
            
            AddForeignKey("dbo.Friendships", "UserId1", "dbo.Users", "Id");
            AddForeignKey("dbo.Games", "AuthorId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Friendships", "UserId1", "dbo.Users");
            DropForeignKey("dbo.UserReviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserReviews", "AuthorId", "dbo.Users");
            DropIndex("dbo.UserReviews", new[] { "UserId" });
            DropIndex("dbo.UserReviews", new[] { "AuthorId" });
            DropTable("dbo.UserReviews");
            AddForeignKey("dbo.Games", "AuthorId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Friendships", "UserId1", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
