namespace Diplom_WebSite_Taras.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Total", c => c.Double(nullable: false));
            AlterColumn("dbo.OrderDetails", "UnitPrice", c => c.Double(nullable: false));
        }
    }
}
