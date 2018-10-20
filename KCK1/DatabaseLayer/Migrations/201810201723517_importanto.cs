namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class importanto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Survey", "isAnonymous", c => c.Boolean(nullable: false));
            DropColumn("dbo.Categories", "isAnonymous");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "isAnonymous", c => c.Boolean(nullable: false));
            DropColumn("dbo.Survey", "isAnonymous");
        }
    }
}
