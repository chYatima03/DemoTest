using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data.Config
{
    public class FactoryConfig : IEntityTypeConfiguration<Factory>
    {
        public void Configure(EntityTypeBuilder<Factory> builder)
        {
            builder.ToTable("Factories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.factoryno).HasMaxLength(100).IsRequired();
            builder.Property(n => n.factoryname).HasMaxLength(250).IsRequired();
            //builder.Property(n => n.status).HasMaxLength(1).IsRequired(false);
            builder.Property(n => n.modifiedby).HasMaxLength(150).IsRequired(false);

        }
    }
}
