namespace WebApplication3.Data.Repository
{
    public interface IFactoryRepository : ICollegeRepository<Factory>
    {
        Task<List<Factory>> GetFactoryByFeeStatusAsync(int feeStatus);
    }

}
