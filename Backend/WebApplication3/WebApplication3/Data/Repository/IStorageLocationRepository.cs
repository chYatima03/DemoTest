namespace WebApplication3.Data.Repository
{
    public interface IStorageLocationRepository : ICollegeRepository<StorageLocation>
    {
        Task<List<StorageLocation>> GetStorageLocationByFeeStatusAsync(int feeStatus);
    }

}
