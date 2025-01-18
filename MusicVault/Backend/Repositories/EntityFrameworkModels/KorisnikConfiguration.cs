using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class KorisnikConfiguration : IEntityTypeConfiguration<Korisnik> {
    public void Configure(EntityTypeBuilder<Korisnik> builder) {
        builder.HasKey(k => k.Id);

        builder.Property(k => k.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(k => k.Ime)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");

        builder.Property(k => k.Prezime)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");

        builder.Property(k => k.Tip)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(k => k.Mejl)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");

        builder.Property(k => k.Telefon)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");

        builder.Property(k => k.GodRodjenja)
            .IsRequired()
            .HasColumnType("date");

        builder.Property(k => k.Pol)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(k => k.Javni)
            .IsRequired()
            .HasColumnType("boolean");

        builder.Property(k => k.Lozinka)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");
    }
}