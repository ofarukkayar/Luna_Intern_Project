namespace Luna_Project_Example.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meters", "maxNumberOfSubs", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meters", "maxNumberOfSubs");
        }
    }
}
