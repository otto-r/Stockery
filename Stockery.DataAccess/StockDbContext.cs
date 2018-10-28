using Stockery.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Stockery.DataAccess
{
    public class StockDbContext : DbContext
    {
        public StockDbContext() : base("StockDb")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // does not pluralize table names
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<HistoricalStockPriceInfo> HistoricalStockPriceInfos { get; set; }
    }
}
