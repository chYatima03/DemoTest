namespace WebApplication3.Data.Repository
{
    public interface ILocationRepository : ICollegeRepository<Location>
    {
        Task<List<Location>> GetLocationByFeeStatusAsync(int feeStatus);
    }

}
