namespace WebApplication3.Data.Repository
{
    public class StorageLocationRepository : CollegeRepository<StorageLocation>, IStorageLocationRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StorageLocationRepository(CollegeDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<StorageLocation>> GetStorageLocationByFeeStatusAsync(int feeStatus)
        {
            //Write code to return students having fee status pending
            return null;
        }
    }

}
