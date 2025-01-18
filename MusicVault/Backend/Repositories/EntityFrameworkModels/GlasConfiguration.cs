using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class GlasConfiguration : IEntityTypeConfiguration<Glas> {
    public void Configure(EntityTypeBuilder<Glas> builder) {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(g => g.Ocena)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(g => g.Datum)
            .IsRequired()
            .HasColumnType("date");

        builder.HasOne(g => g.Korisnik)
            .WithMany()
            .IsRequired();

        builder.HasOne(g => g.MuzickiSadrzaj)
            .WithMany()
            .IsRequired();
    }
}