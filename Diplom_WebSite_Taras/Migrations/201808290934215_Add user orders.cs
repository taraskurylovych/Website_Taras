namespace Diplom_WebSite_Taras.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adduserorders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "UserId");
            AddForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Orders", new[] { "UserId" });
            AlterColumn("dbo.Orders", "UserId", c => c.String());
        }
    }
}
