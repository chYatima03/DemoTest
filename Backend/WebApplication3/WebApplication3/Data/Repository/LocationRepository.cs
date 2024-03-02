namespace WebApplication3.Data.Repository
{
    public class LocationRepository : CollegeRepository<Location>, ILocationRepository
    {
        private readonly CollegeDBContext _dbContext;

        public LocationRepository(CollegeDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Location>> GetLocationByFeeStatusAsync(int feeStatus)
        {
            //Write code to return students having fee status pending
            return null;
        }
    }

}
