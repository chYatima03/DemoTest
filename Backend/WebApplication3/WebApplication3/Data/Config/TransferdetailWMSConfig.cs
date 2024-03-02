using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data.Config
{
    public class TransferdetailWMSConfig : IEntityTypeConfiguration<TransferdetailWMS>
    {
        public void Configure(EntityTypeBuilder<TransferdetailWMS> builder)
        {
            builder.ToTable("Transferdetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.no).IsRequired().HasMaxLength(15);
            builder.Property(n => n.name).IsRequired().HasMaxLength(250);
            builder.Property(n => n.outwmsno).HasMaxLength(10).IsRequired();
            builder.Property(n => n.unit).HasMaxLength(25).IsRequired();
            builder.Property(n => n.outwmsno).HasMaxLength(25).IsRequired();
            builder.Property(n => n.inwmsno).HasMaxLength(25).IsRequired();
            //builder.Property(n => n.currentwmsno).HasMaxLength(25).IsRequired();
            //builder.Property(n => n.stockstatus).HasMaxLength(1).IsRequired(false);
            builder.Property(n => n.modifiedby).HasMaxLength(150).IsRequired(false);

            /*builder.HasOne(n => n.Transfers)
               .WithMany(n => n.Transferdetails)
               .HasForeignKey(n => n.TransferId)
               .HasConstraintName("FK_Transferdetails_Transfers");*/

        }
    }

}
