namespace Diplom_WebSite_Taras.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Checkout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Region", c => c.String());
            DropColumn("dbo.Orders", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "State", c => c.String());
            DropColumn("dbo.Orders", "Region");
        }
    }
}
