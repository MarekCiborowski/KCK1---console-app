namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FollowedUsers", "followedUserID", "dbo.Account");
            DropIndex("dbo.FollowedUsers", new[] { "followedUserID" });
            DropTable("dbo.FollowedUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FollowedUsers",
                c => new
                    {
                        followedUsersID = c.Int(nullable: false, identity: true),
                        followedUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.followedUsersID);
            
            CreateIndex("dbo.FollowedUsers", "followedUserID");
            AddForeignKey("dbo.FollowedUsers", "followedUserID", "dbo.Account", "accountID", cascadeDelete: true);
        }
    }
}
