namespace Diplom_WebSite_Taras.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adddate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DateCreated");
        }
    }
}
