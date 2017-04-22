using System;

namespace UniShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        UniShopDbContext Init();
    }
}