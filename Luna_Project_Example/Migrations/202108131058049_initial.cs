namespace Luna_Project_Example.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meters",
                c => new
                    {
                        serialId = c.Int(nullable: false, identity: true),
                        valveWidth = c.Int(nullable: false),
                        numberOfSubs = c.Int(nullable: false),
                        productionDate = c.DateTime(nullable: false),
                        batteryState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.serialId);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        subsId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        surname = c.String(),
                        meterSerialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.subsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subscribers");
            DropTable("dbo.Meters");
        }
    }
}
