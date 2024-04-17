using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Write.Contacts.Entities;

namespace Write.Data.EF.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(c => c.Id).HasName("user_pkey");

        builder.Property(e => e.AccessLevel)
            .HasMaxLength(100)
            .HasColumnName("access_level")
            .HasConversion(
                v => v.ToString(),
                v => (Access)Enum.Parse(typeof(Access), v));
        
        builder
            .HasMany(c => c.IqAssigned)
            .WithMany(c => c.Users);
    }
}
