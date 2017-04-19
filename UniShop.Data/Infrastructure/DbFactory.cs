using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private UniShopDbContext dbContext;
        public UniShopDbContext Init()
        {
            return dbContext ?? (dbContext = new UniShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
