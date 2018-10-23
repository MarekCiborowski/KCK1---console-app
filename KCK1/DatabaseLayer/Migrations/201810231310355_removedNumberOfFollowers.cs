namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedNumberOfFollowers : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Account", "followers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Account", "followers", c => c.Int(nullable: false));
        }
    }
}
