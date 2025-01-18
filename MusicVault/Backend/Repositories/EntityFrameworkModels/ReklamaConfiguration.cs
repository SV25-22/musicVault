using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class ReklamaConfiguration : IEntityTypeConfiguration<Reklama> {
    public void Configure(EntityTypeBuilder<Reklama> builder) {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(r => r.Cena)
            .HasColumnType("integer");

        builder.HasOne(r => r.MultimedijalniSadrzaj)
            .WithMany();

        builder.HasMany(r => r.Izvodjaci)
            .WithMany();
    }
}