using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication3.Data.Config
{
    public class StorageLocationConfig : IEntityTypeConfiguration<StorageLocation>
    {
        public void Configure(EntityTypeBuilder<StorageLocation> builder)
        {
            builder.ToTable("StorageLocations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.no).IsRequired();
            builder.Property(n => n.no).HasMaxLength(250);
            builder.Property(n => n.name).IsRequired().HasMaxLength(500);

            builder.Property(n => n.modifiedby).IsRequired(false).HasMaxLength(150);

            /*builder.HasOne(n => n.Location)
                   .WithMany(n => n.Students)
                   .HasForeignKey(n => n.LocationId)
                   .HasConstraintName("FK_StorageLocations_Students_Location");
        */
        }
    }
}
