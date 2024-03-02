namespace WebApplication3.Data.Repository
{
    public class DocumentRepository : CollegeRepository<Document>, IDocumentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public DocumentRepository(CollegeDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Document>> GetDocumentByFeeStatusAsync(int feeStatus)
        {
            //Write code to return students having fee status pending
            return null;
        }
    }
}
