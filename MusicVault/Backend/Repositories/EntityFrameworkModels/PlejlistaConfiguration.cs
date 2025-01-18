using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class PlejlistaConfiguration : IEntityTypeConfiguration<Plejlista> {
    public void Configure(EntityTypeBuilder<Plejlista> builder) {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(p => p.Naziv)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");

        builder.HasMany(p => p.Zanrovi)
            .WithMany();

        builder.HasMany(p => p.MuzickiSadrzaji)
            .WithMany();
    }
}