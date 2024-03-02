using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data.Config
{
    public class DocumentConfig : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.docno).IsRequired().HasMaxLength(100);
            builder.Property(n => n.docname).HasMaxLength(250).IsRequired();
            //builder.Property(n => n.status).HasMaxLength(1).IsRequired(false);
            builder.Property(n => n.modifiedby).HasMaxLength(150).IsRequired(false);


        }
    }
}
