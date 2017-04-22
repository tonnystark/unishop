namespace UniShop.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}