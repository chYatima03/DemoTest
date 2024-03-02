using Microsoft.EntityFrameworkCore;
using WebApplication3.Data.Config;

namespace WebApplication3.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<StorageLocation> StorageLocations { get; set; }
        public DbSet<Stock> Stock { get; set; }

        public DbSet<TransferWMS> Transfers { get; set; }
        public DbSet<TransferdetailWMS> Transferdetials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Table 1
            modelBuilder.ApplyConfiguration(new StudentConfig());
            //Table 2
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            //modelBuilder.ApplyConfiguration(new New1Config());
            //Table 3
            modelBuilder.ApplyConfiguration(new DocumentConfig());
            modelBuilder.ApplyConfiguration(new FactoryConfig());
            modelBuilder.ApplyConfiguration(new LocationConfig());
            modelBuilder.ApplyConfiguration(new StorageLocationConfig());
            modelBuilder.ApplyConfiguration(new StockConfig());
            modelBuilder.ApplyConfiguration(new TransferWMSConfig());
            modelBuilder.ApplyConfiguration(new TransferdetailWMSConfig());
            //modelBuilder.ApplyConfiguration(new New2Config());
        }
    }
}
