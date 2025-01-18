using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model.Recenzija;

namespace MusicVault.Backend.Courses.Repositories.EntityFrameworkConfigurations;

public class RecenzijaConfiguration : IEntityTypeConfiguration<Recenzija> {
    public void Configure(EntityTypeBuilder<Recenzija> builder) {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(r => r.Ocena)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(r => r.Opis)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnType("varchar");

        builder.Property(r => r.Objavljena)
            .IsRequired()
            .HasColumnType("boolean");

        builder.HasOne(r => r.Urednik)
            .WithMany();

        builder.HasOne(r => r.MuzickiSadrzaj)
            .WithMany()
            .IsRequired();

        builder.Property(r => r.Stanje)
            .IsRequired()
            .HasColumnType("integer");

        builder.Ignore(r => r.StanjeRecenzije);
    }
}