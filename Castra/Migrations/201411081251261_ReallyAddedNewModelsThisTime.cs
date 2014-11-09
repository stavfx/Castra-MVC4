namespace Castra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReallyAddedNewModelsThisTime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Price = c.Int(nullable: false),
                        Supplier_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_ID)
                .Index(t => t.Supplier_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "Supplier_ID" });
            DropForeignKey("dbo.Products", "Supplier_ID", "dbo.Suppliers");
            DropTable("dbo.Products");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Branches");
        }
    }
}
