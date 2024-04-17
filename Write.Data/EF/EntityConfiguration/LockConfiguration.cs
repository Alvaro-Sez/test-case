using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Write.Contacts.Entities;

namespace Write.Data.EF.EntityConfiguration;

public class LockConfiguration : IEntityTypeConfiguration<Lock>
{
    public void Configure(EntityTypeBuilder<Lock> builder)
    {
        builder.ToTable("locks");

        builder.HasKey(c => c.Id).HasName("lock_pkey");

        builder.Property(e => e.AccessLevel)
            .HasMaxLength(100)
            .HasColumnName("access_level")
            .HasConversion(
                v => v.ToString(),
                v => (Access)Enum.Parse(typeof(Access), v));
    }
}