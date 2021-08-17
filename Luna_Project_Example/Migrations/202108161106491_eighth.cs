namespace Luna_Project_Example.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eighth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscribers", "meter_meterID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscribers", "meter_meterID", c => c.Int(nullable: false));
        }
    }
}
