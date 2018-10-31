namespace DatabaseLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DziwkiMigracjeKaskady : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonDatas", "zipcode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonDatas", "zipcode", c => c.Int(nullable: false));
        }
    }
}
