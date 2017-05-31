namespace UniShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproducttag : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Tags", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Tags", c => c.String());
        }
    }
}
