namespace UniShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addContent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Slides", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Slides", "Description", c => c.String(maxLength: 256));
        }
    }
}
