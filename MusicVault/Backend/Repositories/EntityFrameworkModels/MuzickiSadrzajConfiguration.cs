using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model.MuzickiSadrzaj;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class MuzickiSadrzajConfiguration : IEntityTypeConfiguration<MuzickiSadrzaj> {
    public void Configure(EntityTypeBuilder<MuzickiSadrzaj> builder) {
        builder.HasKey(ms => ms.Id);

        builder.Property(ms => ms.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(ms => ms.Opis)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");

        builder.Property(ms => ms.Objavljeno)
            .HasColumnType("boolean");

        builder.HasMany(ms => ms.Zanrevi)
            .WithMany();

        builder.HasMany(ms => ms.MuzickiSadrzaji)
            .WithMany();

        builder.HasMany(ms => ms.MultimedijalniSadrzaji)
            .WithMany();

        builder.HasMany(ms => ms.Izvodjaci)
            .WithMany();

        builder
            .HasDiscriminator<string>("Vrsta")
            .HasValue<Album>("Album")
            .HasValue<Delo>("Delo")
            .HasValue<Nastup>("Nastup");
    }
}

public class AlbumConfiguration : IEntityTypeConfiguration<Album> {
    public void Configure(EntityTypeBuilder<Album> builder) {
        builder.Property(n => n.Tip)
            .HasColumnType("integer");
    }
}