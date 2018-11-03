namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class votes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Votes", "accountID", "dbo.Account");
            DropIndex("dbo.Votes", new[] { "accountID" });
            AlterColumn("dbo.Votes", "accountID", c => c.Int(nullable: false));
            CreateIndex("dbo.Votes", "accountID");
            AddForeignKey("dbo.Votes", "accountID", "dbo.Account", "accountID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "accountID", "dbo.Account");
            DropIndex("dbo.Votes", new[] { "accountID" });
            AlterColumn("dbo.Votes", "accountID", c => c.Int());
            CreateIndex("dbo.Votes", "accountID");
            AddForeignKey("dbo.Votes", "accountID", "dbo.Account", "accountID");
        }
    }
}
