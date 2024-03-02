namespace WebApplication3.Data.Repository
{
    public interface IStockRepository : ICollegeRepository<Stock>
    {
        Task<List<Stock>> GetStockByFeeStatusAsync(int feeStatus);
    }

}
