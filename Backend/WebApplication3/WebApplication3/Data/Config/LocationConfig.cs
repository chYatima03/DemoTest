using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data.Config
{
    public class LocationConfig : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.zone).HasMaxLength(4).IsRequired();
            builder.Property(n => n.layer).HasMaxLength(4).IsRequired();
            builder.Property(n => n.road).HasMaxLength(4).IsRequired();
            builder.Property(n => n.column).HasMaxLength(4).IsRequired();
            builder.Property(n => n.row).HasMaxLength(4).IsRequired();
            builder.Property(n => n.position).HasMaxLength(4).IsRequired();
            //builder.Property(n => n.status).HasMaxLength(1).IsRequired(false);
            builder.Property(n => n.modifiedby).HasMaxLength(150).IsRequired(false);
        }
    }
}
