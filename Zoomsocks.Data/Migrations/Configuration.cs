namespace Zoomsocks.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Zoomsocks.Data.ZoomsocksDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Zoomsocks.Data.ZoomsocksDbContext context)
        {
            
        }
    }
}
