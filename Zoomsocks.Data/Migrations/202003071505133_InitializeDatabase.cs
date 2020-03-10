namespace Zoomsocks.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.product-category",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Alias = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.product",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Alias = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        ProductCategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.product-category", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.product", "ProductCategoryId", "dbo.product-category");
            DropIndex("dbo.product", new[] { "ProductCategoryId" });
            DropTable("dbo.product");
            DropTable("dbo.product-category");
        }
    }
}
