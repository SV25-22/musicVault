using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class GlasanjeConfiguration : IEntityTypeConfiguration<Glasanje> {
    public void Configure(EntityTypeBuilder<Glasanje> builder) {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(g => g.Naziv)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");

        builder.Property(g => g.Aktivno)
            .IsRequired()
            .HasColumnType("boolean");

        builder.Property(g => g.PocetakGlasanja)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(g => g.KrajGlasanja)
            .IsRequired()
            .HasColumnType("date");

        builder.HasMany(g => g.Glasovi)
            .WithOne();

        builder.HasMany(g => g.OpcijeZaGlasanje)
            .WithMany();
    }
}