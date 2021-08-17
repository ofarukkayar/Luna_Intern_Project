namespace Luna_Project_Example.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        subscription_id = c.Int(nullable: false, identity: true),
                        m_id = c.Int(nullable: false),
                        s_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.subscription_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subscriptions");
        }
    }
}
