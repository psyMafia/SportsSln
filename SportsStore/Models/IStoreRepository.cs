namespace SportsStore.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;
        public EFStoreRepository(StoreDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
    }
}
