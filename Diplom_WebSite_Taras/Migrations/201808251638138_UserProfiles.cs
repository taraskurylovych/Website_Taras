namespace Diplom_WebSite_Taras.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProfiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblUserProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblUserProfiles", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.tblUserProfiles", new[] { "Id" });
            DropTable("dbo.tblUserProfiles");
        }
    }
}
