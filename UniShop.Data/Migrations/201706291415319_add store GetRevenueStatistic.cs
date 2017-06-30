namespace UniShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addstoreGetRevenueStatistic : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("spGetRevenueStatistic", p => new
            {
                fromDate = p.String(),
                toDate = p.String()
            },
                @"SELECT o.CreateDate AS 'Date', SUM(od.Quantity * od.Price) AS 'Revenues', SUM((od.Quantity * od.Price) - (od.Quantity * p.OriginalPrice)) AS 'Benefit'
                  FROM dbo.OrderDetails od
                  INNER JOIN dbo.Orders o
                  ON od.OrderID = o.ID
                  INNER JOIN dbo.Products p
                  ON p.ID = od.ProductID
                  WHERE o.CreateDate >= CAST(@fromDate AS DATE) AND o.CreateDate <= CAST(@toDate AS DATE)
                  GROUP BY o.CreateDate
                ");
        }

        public override void Down()
        {
            DropStoredProcedure("spGetRevenueStatistic");
        }
    }
}
