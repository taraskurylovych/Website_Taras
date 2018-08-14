namespace Diplom_WebSite_Taras.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dateremove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "DateCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
