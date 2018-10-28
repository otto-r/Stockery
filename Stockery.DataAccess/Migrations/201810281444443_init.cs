namespace Stockery.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoricalStockPriceInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Stock_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stocks", t => t.Stock_Id)
                .Index(t => t.Stock_Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Ticker = c.String(nullable: false, maxLength: 8),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoricalStockPriceInfoes", "Stock_Id", "dbo.Stocks");
            DropIndex("dbo.HistoricalStockPriceInfoes", new[] { "Stock_Id" });
            DropTable("dbo.Stocks");
            DropTable("dbo.HistoricalStockPriceInfoes");
        }
    }
}
