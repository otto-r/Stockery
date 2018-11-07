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
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Bond> Bonds { get; set; }
    }
}
