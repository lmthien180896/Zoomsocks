using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomsocks.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ZoomsocksDbContext dbContext;

        public ZoomsocksDbContext Init()
        {
            return dbContext ?? (dbContext = new ZoomsocksDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
