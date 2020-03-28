namespace Zoomsocks.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreImagesToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.product", "MoreImages", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.product", "MoreImages");
        }
    }
}
