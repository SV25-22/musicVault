using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class IzvodjacConfiguration : IEntityTypeConfiguration<Izvodjac> {
    public void Configure(EntityTypeBuilder<Izvodjac> builder) {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(i => i.Opis)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");

        builder.HasMany(i => i.MultimedijalniSadrzaji)
            .WithMany();

        builder.HasMany(i => i.Zanrevi)
            .WithMany();
    }
}