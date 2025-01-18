using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class ZanrConfiguration : IEntityTypeConfiguration<Zanr> {
    public void Configure(EntityTypeBuilder<Zanr> builder) {
        builder.HasKey(z => z.Id);

        builder.Property(z => z.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(z => z.Naziv)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");

        builder.HasOne(z => z.NadZanr)
            .WithOne();
    }
}