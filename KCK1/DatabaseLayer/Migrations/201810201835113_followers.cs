namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class followers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FollowerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FollowerId })
                .ForeignKey("dbo.Account", t => t.UserId)
                .ForeignKey("dbo.Account", t => t.FollowerId)
                .Index(t => t.UserId)
                .Index(t => t.FollowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followers", "FollowerId", "dbo.Account");
            DropForeignKey("dbo.Followers", "UserId", "dbo.Account");
            DropIndex("dbo.Followers", new[] { "FollowerId" });
            DropIndex("dbo.Followers", new[] { "UserId" });
            DropTable("dbo.Followers");
        }
    }
}
