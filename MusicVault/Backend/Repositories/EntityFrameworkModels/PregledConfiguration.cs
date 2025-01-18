using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class PregledConfiguration : IEntityTypeConfiguration<Pregled> {
    public void Configure(EntityTypeBuilder<Pregled> builder) {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(p => p.Datum)
            .IsRequired()
            .HasColumnType("date");

        builder.HasOne(p => p.Korisnik)
            .WithMany()
            .IsRequired();

        builder.HasOne(p => p.MuzickiSadrzaj)
            .WithMany()
            .IsRequired();
    }
}