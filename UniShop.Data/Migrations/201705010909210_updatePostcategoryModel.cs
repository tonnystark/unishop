using System.Data.Entity.Migrations;

namespace UniShop.Data.Migrations
{
    public partial class updatePostcategoryModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostCategories", "Description", c => c.String(maxLength: 500));
        }

        public override void Down()
        {
            DropColumn("dbo.PostCategories", "Description");
        }
    }
}