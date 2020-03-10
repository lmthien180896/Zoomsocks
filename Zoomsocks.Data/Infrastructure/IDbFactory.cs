using System;

namespace Zoomsocks.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ZoomsocksDbContext Init();
    }
}