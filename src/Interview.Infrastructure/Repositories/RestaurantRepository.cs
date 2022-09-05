namespace Infrastructure.Data.Repositories
{
    public class RestaurantRepository : RepositoryBase<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}