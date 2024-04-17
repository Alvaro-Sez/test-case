using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Write.Contacts.Entities;

namespace Write.Data.EF.EntityConfiguration;

public class IqConfiguration: IEntityTypeConfiguration<Iq>
{
    public void Configure(EntityTypeBuilder<Iq> builder)
    {
        builder.ToTable("iqs");

        builder.HasKey(c => c.Id).HasName("iq_pkey");

        builder.Property(c => c.BuildingName)
                    .HasMaxLength(50)
                    .HasColumnName("building_name").IsRequired();
        
        builder.HasIndex(c => c.BuildingName).IsUnique();
    }
}
