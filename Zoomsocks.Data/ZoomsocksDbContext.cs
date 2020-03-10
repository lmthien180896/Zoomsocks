using System.Data.Entity;
using Zoomsocks.Model.Models;

namespace Zoomsocks.Data
{
    public class ZoomsocksDbContext : DbContext
    {
        public ZoomsocksDbContext() : base("ZoomsocksConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Product> Products { set; get; }

        public DbSet<ProductCategory> ProductCategories { set; get; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
        }
    }
}