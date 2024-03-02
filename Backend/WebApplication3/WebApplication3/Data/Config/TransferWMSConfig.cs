using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data.Config
{
    public class TransferWMSConfig : IEntityTypeConfiguration<TransferWMS>
    {
        public void Configure(EntityTypeBuilder<TransferWMS> builder)
        {
            builder.ToTable("Transfers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.no).IsRequired().HasMaxLength(15);
            builder.Property(n => n.outstore).HasMaxLength(25).IsRequired();
            builder.Property(n => n.instore).HasMaxLength(25).IsRequired();
            builder.Property(n => n.currentwmsno).HasMaxLength(25).IsRequired();
            //builder.Property(n => n.stockstatus).HasMaxLength(1).IsRequired(false);
            builder.Property(n => n.modifiedby).HasMaxLength(150).IsRequired(false);

            builder.HasOne(n => n.Document)
               .WithMany(n => n.Transfers)
               .HasForeignKey(n => n.DocumentId)
               .HasConstraintName("FK_Transfers_Document");
        }
    }
}
