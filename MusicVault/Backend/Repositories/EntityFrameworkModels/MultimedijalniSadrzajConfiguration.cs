using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model.MultimedijalniSadrzaj;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class MultimedijalniSadrzajConfiguration : IEntityTypeConfiguration<MultimedijalniSadrzaj> {
    public void Configure(EntityTypeBuilder<MultimedijalniSadrzaj> builder) {
        builder.HasKey(ms => ms.Id);

        builder.Property(ms => ms.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(ms => ms.Link)
            .HasMaxLength(255)
            .IsRequired()
            .HasColumnType("varchar");

        builder
            .HasDiscriminator<string>("Vrsta")
            .HasValue<Gif>("Album")
            .HasValue<Slika>("Delo")
            .HasValue<Video>("Nastup");
    }
}