namespace WebApplication3.Data.Repository
{
    public interface IDocumentRepository : ICollegeRepository<Document>
    {
        Task<List<Document>> GetDocumentByFeeStatusAsync(int feeStatus);
    }
}
