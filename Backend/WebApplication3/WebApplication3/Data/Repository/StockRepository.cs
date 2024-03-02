namespace WebApplication3.Data.Repository
{
    public class StockRepository : CollegeRepository<Stock>, IStockRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StockRepository(CollegeDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Stock>> GetStockByFeeStatusAsync(int feeStatus)
        {
            //Write code to return students having fee status pending
            return null;
        }
    }
}
