using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Write.Contacts.Entities;

namespace Write.Data.EF.EntityConfiguration;

public class BindRequestConfiguration: IEntityTypeConfiguration<BindIqRequest>
{
    public void Configure(EntityTypeBuilder<BindIqRequest> builder)
    {
        builder.ToTable("bind_req");
        builder.HasKey(c => c.Id).HasName("bind_req_pkey");
        builder.Property(c => c.AuthorId).HasColumnName("author_id");
        builder.Property(c => c.IqId).HasColumnName("iq_id");
    }
}