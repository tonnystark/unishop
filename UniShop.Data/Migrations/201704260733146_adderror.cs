using System.Data.Entity.Migrations;

namespace UniShop.Data.Migrations
{
    public partial class adderror : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Errors",
                c => new
                {
                    ID = c.Int(false, true),
                    Message = c.String(),
                    StackTrace = c.String(),
                    CreatedDate = c.DateTime(false)
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropTable("dbo.Errors");
        }
    }
}