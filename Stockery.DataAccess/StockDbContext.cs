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

        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // does not pluralize table names
        }
    }
}
