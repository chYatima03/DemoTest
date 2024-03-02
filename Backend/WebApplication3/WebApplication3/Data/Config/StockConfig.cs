using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data.Config
{
    public class StockConfig : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.no).IsRequired().HasMaxLength(25);
            builder.Property(n => n.lotno).HasMaxLength(15).IsRequired();
            builder.Property(n => n.name).HasMaxLength(150).IsRequired();

            builder.Property(n => n.qty).HasMaxLength(10).IsRequired();
            builder.Property(n => n.unit).HasMaxLength(10).IsRequired();
            builder.Property(n => n.expiredate).HasMaxLength(10).IsRequired();
            builder.Property(n => n.currentwmsno).HasMaxLength(25).IsRequired();
            //builder.Property(n => n.stockstatus).HasMaxLength(1).IsRequired(false);
            builder.Property(n => n.modifiedby).HasMaxLength(150).IsRequired(false);


        }
    }
}
