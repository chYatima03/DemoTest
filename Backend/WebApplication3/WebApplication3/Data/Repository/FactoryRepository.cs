namespace WebApplication3.Data.Repository
{
    public class FactoryRepository : CollegeRepository<Factory>, IFactoryRepository
    {
        private readonly CollegeDBContext _dbContext;

        public FactoryRepository(CollegeDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Factory>> GetFactoryByFeeStatusAsync(int feeStatus)
        {
            //Write code to return students having fee status pending
            return null;
        }
    }

}
