using System.Data.Entity.Migrations;

namespace UniShop.Data.Migrations
{
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Footers",
                c => new
                {
                    ID = c.String(false, 50),
                    Content = c.String(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.MenuGroups",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(false, 50)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Menus",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(false, 50),
                    URL = c.String(false, 256),
                    DisplayOrder = c.Int(),
                    GroupID = c.Int(false),
                    Target = c.String(maxLength: 10),
                    Status = c.Boolean(false)
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MenuGroups", t => t.GroupID, true)
                .Index(t => t.GroupID);

            CreateTable(
                "dbo.OrderDetails",
                c => new
                {
                    OrderID = c.Int(false),
                    ProductID = c.Int(false),
                    Quantity = c.Int(false)
                })
                .PrimaryKey(t => new {t.OrderID, t.ProductID})
                .ForeignKey("dbo.Orders", t => t.OrderID, true)
                .ForeignKey("dbo.Products", t => t.ProductID, true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);

            CreateTable(
                "dbo.Orders",
                c => new
                {
                    ID = c.Int(false, true),
                    CustomerName = c.String(false, 256),
                    CustomerAddress = c.String(false, 256),
                    CustomerEmail = c.String(false, 256),
                    CustomerMobile = c.String(false, 50),
                    CustomerMessage = c.String(false, 256),
                    CreateDate = c.DateTime(),
                    CreateBy = c.String(),
                    PaymentMethod = c.String(maxLength: 256),
                    PaymentStatus = c.String(),
                    Status = c.Boolean(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Products",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(false, 256),
                    Alias = c.String(false, 256),
                    CategoryID = c.Int(false),
                    Image = c.String(maxLength: 256),
                    MoreImages = c.String(storeType: "xml"),
                    Price = c.Decimal(false, 18, 2),
                    PromotionPrice = c.Decimal(precision: 18, scale: 2),
                    Warranty = c.Int(),
                    Description = c.String(maxLength: 500),
                    Content = c.String(),
                    HomeFlag = c.Boolean(),
                    HotFlag = c.Boolean(),
                    ViewCount = c.Int(),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(maxLength: 256),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(maxLength: 256),
                    MetaKeyword = c.String(maxLength: 256),
                    MetaDescription = c.String(maxLength: 256),
                    Status = c.Boolean(false)
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductCategories", t => t.CategoryID, true)
                .Index(t => t.CategoryID);

            CreateTable(
                "dbo.ProductCategories",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(false, 256),
                    Alias = c.String(false, 256),
                    Description = c.String(maxLength: 500),
                    ParentID = c.Int(),
                    DisplayOrder = c.Int(),
                    Image = c.String(maxLength: 256),
                    HomeFlag = c.Boolean(),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(maxLength: 256),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(maxLength: 256),
                    MetaKeyword = c.String(maxLength: 256),
                    MetaDescription = c.String(maxLength: 256),
                    Status = c.Boolean(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Pages",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(false, 256),
                    Alias = c.String(false, 256, unicode: false),
                    Content = c.String(),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(maxLength: 256),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(maxLength: 256),
                    MetaKeyword = c.String(maxLength: 256),
                    MetaDescription = c.String(maxLength: 256),
                    Status = c.Boolean(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.PostCategories",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(false, 256),
                    Alias = c.String(false, 256, unicode: false),
                    ParentID = c.Int(),
                    DisplayOrder = c.Int(),
                    Image = c.String(maxLength: 256),
                    HomeFlag = c.Boolean(),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(maxLength: 256),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(maxLength: 256),
                    MetaKeyword = c.String(maxLength: 256),
                    MetaDescription = c.String(maxLength: 256),
                    Status = c.Boolean(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Posts",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(false, 256),
                    Alias = c.String(false, 256, unicode: false),
                    CategoryID = c.Int(false),
                    Image = c.String(maxLength: 256),
                    Description = c.String(maxLength: 500),
                    Content = c.String(),
                    HomeFlag = c.Boolean(),
                    HotFlag = c.Boolean(),
                    ViewCount = c.Int(),
                    CreatedDate = c.DateTime(),
                    CreatedBy = c.String(maxLength: 256),
                    UpdatedDate = c.DateTime(),
                    UpdatedBy = c.String(maxLength: 256),
                    MetaKeyword = c.String(maxLength: 256),
                    MetaDescription = c.String(maxLength: 256),
                    Status = c.Boolean(false)
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PostCategories", t => t.CategoryID, true)
                .Index(t => t.CategoryID);

            CreateTable(
                "dbo.PostTags",
                c => new
                {
                    PostID = c.Int(false),
                    TagID = c.String(false, 50, unicode: false)
                })
                .PrimaryKey(t => new {t.PostID, t.TagID})
                .ForeignKey("dbo.Posts", t => t.PostID, true)
                .ForeignKey("dbo.Tags", t => t.TagID, true)
                .Index(t => t.PostID)
                .Index(t => t.TagID);

            CreateTable(
                "dbo.Tags",
                c => new
                {
                    ID = c.String(false, 50, unicode: false),
                    Name = c.String(false, 50),
                    Type = c.String(false, 50)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.ProductTags",
                c => new
                {
                    ProductID = c.Int(false),
                    TagID = c.String(false, 50, unicode: false)
                })
                .PrimaryKey(t => new {t.ProductID, t.TagID})
                .ForeignKey("dbo.Products", t => t.ProductID, true)
                .ForeignKey("dbo.Tags", t => t.TagID, true)
                .Index(t => t.ProductID)
                .Index(t => t.TagID);

            CreateTable(
                "dbo.Slides",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(false, 256),
                    Description = c.String(maxLength: 256),
                    Image = c.String(maxLength: 256),
                    Url = c.String(maxLength: 256),
                    DisplayOrder = c.Int(),
                    Status = c.Boolean(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.SupportOnlines",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(false, 50),
                    Department = c.String(maxLength: 50),
                    Skype = c.String(maxLength: 50),
                    Mobile = c.String(maxLength: 50),
                    Email = c.String(maxLength: 50),
                    Yahoo = c.String(maxLength: 50),
                    Facebook = c.String(maxLength: 50),
                    Status = c.Boolean(false),
                    DisplayOrder = c.Int()
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.SystemConfigs",
                c => new
                {
                    ID = c.Int(false, true),
                    Code = c.String(false, 50, unicode: false),
                    ValueString = c.String(maxLength: 50),
                    ValueInt = c.Int()
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.VisitorStatistics",
                c => new
                {
                    ID = c.Guid(false),
                    VisitedDate = c.DateTime(false),
                    IPAddress = c.String(maxLength: 50)
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ProductTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.ProductTags", "ProductID", "dbo.Products");
            DropForeignKey("dbo.PostTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.PostTags", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.ProductCategories");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Menus", "GroupID", "dbo.MenuGroups");
            DropIndex("dbo.ProductTags", new[] {"TagID"});
            DropIndex("dbo.ProductTags", new[] {"ProductID"});
            DropIndex("dbo.PostTags", new[] {"TagID"});
            DropIndex("dbo.PostTags", new[] {"PostID"});
            DropIndex("dbo.Posts", new[] {"CategoryID"});
            DropIndex("dbo.Products", new[] {"CategoryID"});
            DropIndex("dbo.OrderDetails", new[] {"ProductID"});
            DropIndex("dbo.OrderDetails", new[] {"OrderID"});
            DropIndex("dbo.Menus", new[] {"GroupID"});
            DropTable("dbo.VisitorStatistics");
            DropTable("dbo.SystemConfigs");
            DropTable("dbo.SupportOnlines");
            DropTable("dbo.Slides");
            DropTable("dbo.ProductTags");
            DropTable("dbo.Tags");
            DropTable("dbo.PostTags");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.Pages");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Menus");
            DropTable("dbo.MenuGroups");
            DropTable("dbo.Footers");
        }
    }
}