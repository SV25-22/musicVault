using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicVault.Backend.Model;
using MusicVault.Backend.Model.MultimedijalniSadrzaj;
using MusicVault.Backend.Model.MuzickiSadrzaj;
using MusicVault.Backend.Model.Recenzija;
using System.Reflection;

public class SqlDbContext : DbContext {
    private readonly IConfiguration _configuration;
    public SqlDbContext() { }

    public SqlDbContext(DbContextOptions<SqlDbContext> options, IConfiguration configuration)
        : base(options) {
        _configuration = configuration;
    }

    public DbSet<MultimedijalniSadrzaj> MultimedijalniSadrzaj { get; set; }
    public DbSet<Gif> Gif { get; set; }
    public DbSet<Slika> Slika { get; set; }
    public DbSet<Video> Video { get; set; }


    public DbSet<MuzickiSadrzaj> MuzickiSadrzaj { get; set; }
    public DbSet<Album> Album { get; set; }
    public DbSet<Delo> Delo { get; set; }
    public DbSet<Nastup> Nastup { get; set; }

    public DbSet<Korisnik> Korisnik { get; set; }
    public DbSet<Recenzija> Recenzija { get; set; }
    public DbSet<Zanr> Zanr { get; set; }
    public DbSet<Izvodjac> Izvodjac { get; set; }
    public DbSet<Plejlista> Plejlista { get; set; }
    public DbSet<Glas> Glas { get; set; }
    public DbSet<Glasanje> Glasanje { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        if (!optionsBuilder.IsConfigured) {
            var connectionString = "Host=localhost;" +
                                   "Port=5432;" +
                                   "Database=MusicVault;" +
                                   "Include Error Detail = true;" +
                                   "User Id = postgres;" +
                                   "Password=123;";

            optionsBuilder.UseNpgsql(connectionString)
                .LogTo(s => { System.Diagnostics.Debug.WriteLine(s); })
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
