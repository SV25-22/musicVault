using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MusicVault.Migrations
{
    /// <inheritdoc />
    public partial class Migracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Glasanje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PocetakGlasanja = table.Column<DateOnly>(type: "date", nullable: false),
                    KrajGlasanja = table.Column<DateOnly>(type: "date", nullable: false),
                    Aktivno = table.Column<bool>(type: "boolean", nullable: false),
                    Naziv = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glasanje", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Izvodjac",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Opis = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izvodjac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ime = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Prezime = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Tip = table.Column<int>(type: "integer", nullable: false),
                    Mejl = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Telefon = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    GodRodjenja = table.Column<DateOnly>(type: "date", nullable: false),
                    Pol = table.Column<int>(type: "integer", nullable: false),
                    Lozinka = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Javni = table.Column<bool>(type: "boolean", nullable: false),
                    Banovan = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MultimedijalniSadrzaj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Link = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Vrsta = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimedijalniSadrzaj", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MuzickiSadrzaj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Opis = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Objavljeno = table.Column<bool>(type: "boolean", nullable: false),
                    Vrsta = table.Column<string>(type: "text", nullable: false),
                    Tip = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuzickiSadrzaj", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plejlista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Naziv = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plejlista", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zanr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NadZanrID = table.Column<int>(type: "integer", nullable: true),
                    Naziv = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanr", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zanr_Zanr_NadZanrID",
                        column: x => x.NadZanrID,
                        principalTable: "Zanr",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IzvodjacMultimedijalniSadrzaj",
                columns: table => new
                {
                    IzvodjacId = table.Column<int>(type: "integer", nullable: false),
                    MultimedijalniSadrzajiId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzvodjacMultimedijalniSadrzaj", x => new { x.IzvodjacId, x.MultimedijalniSadrzajiId });
                    table.ForeignKey(
                        name: "FK_IzvodjacMultimedijalniSadrzaj_Izvodjac_IzvodjacId",
                        column: x => x.IzvodjacId,
                        principalTable: "Izvodjac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IzvodjacMultimedijalniSadrzaj_MultimedijalniSadrzaj_Multime~",
                        column: x => x.MultimedijalniSadrzajiId,
                        principalTable: "MultimedijalniSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reklama",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MultimedijalniSadrzajId = table.Column<int>(type: "integer", nullable: false),
                    Cena = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reklama", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reklama_MultimedijalniSadrzaj_MultimedijalniSadrzajId",
                        column: x => x.MultimedijalniSadrzajId,
                        principalTable: "MultimedijalniSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Glas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KorisnikId = table.Column<int>(type: "integer", nullable: false),
                    MuzickiSadrzajId = table.Column<int>(type: "integer", nullable: false),
                    Datum = table.Column<DateOnly>(type: "date", nullable: false),
                    Ocena = table.Column<int>(type: "integer", nullable: false),
                    GlasanjeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Glas_Glasanje_GlasanjeId",
                        column: x => x.GlasanjeId,
                        principalTable: "Glasanje",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Glas_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Glas_MuzickiSadrzaj_MuzickiSadrzajId",
                        column: x => x.MuzickiSadrzajId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlasanjeMuzickiSadrzaj",
                columns: table => new
                {
                    GlasanjeId = table.Column<int>(type: "integer", nullable: false),
                    OpcijeZaGlasanjeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlasanjeMuzickiSadrzaj", x => new { x.GlasanjeId, x.OpcijeZaGlasanjeId });
                    table.ForeignKey(
                        name: "FK_GlasanjeMuzickiSadrzaj_Glasanje_GlasanjeId",
                        column: x => x.GlasanjeId,
                        principalTable: "Glasanje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlasanjeMuzickiSadrzaj_MuzickiSadrzaj_OpcijeZaGlasanjeId",
                        column: x => x.OpcijeZaGlasanjeId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Izvodi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uloga = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    IzvodjacId = table.Column<int>(type: "integer", nullable: false),
                    MuzickiSadrzajId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izvodi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Izvodi_Izvodjac_IzvodjacId",
                        column: x => x.IzvodjacId,
                        principalTable: "Izvodjac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Izvodi_MuzickiSadrzaj_MuzickiSadrzajId",
                        column: x => x.MuzickiSadrzajId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultimedijalniSadrzajMuzickiSadrzaj",
                columns: table => new
                {
                    MultimedijalniSadrzajiId = table.Column<int>(type: "integer", nullable: false),
                    MuzickiSadrzajId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimedijalniSadrzajMuzickiSadrzaj", x => new { x.MultimedijalniSadrzajiId, x.MuzickiSadrzajId });
                    table.ForeignKey(
                        name: "FK_MultimedijalniSadrzajMuzickiSadrzaj_MultimedijalniSadrzaj_M~",
                        column: x => x.MultimedijalniSadrzajiId,
                        principalTable: "MultimedijalniSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultimedijalniSadrzajMuzickiSadrzaj_MuzickiSadrzaj_MuzickiS~",
                        column: x => x.MuzickiSadrzajId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MuzickiSadrzajMuzickiSadrzaj",
                columns: table => new
                {
                    MuzickiSadrzajId = table.Column<int>(type: "integer", nullable: false),
                    MuzickiSadrzajiId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuzickiSadrzajMuzickiSadrzaj", x => new { x.MuzickiSadrzajId, x.MuzickiSadrzajiId });
                    table.ForeignKey(
                        name: "FK_MuzickiSadrzajMuzickiSadrzaj_MuzickiSadrzaj_MuzickiSadrzajId",
                        column: x => x.MuzickiSadrzajId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuzickiSadrzajMuzickiSadrzaj_MuzickiSadrzaj_MuzickiSadrzaji~",
                        column: x => x.MuzickiSadrzajiId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pregled",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KorisnikId = table.Column<int>(type: "integer", nullable: false),
                    MuzickiSadrzajId = table.Column<int>(type: "integer", nullable: false),
                    Datum = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregled", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pregled_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pregled_MuzickiSadrzaj_MuzickiSadrzajId",
                        column: x => x.MuzickiSadrzajId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UrednikId = table.Column<int>(type: "integer", nullable: true),
                    MuzickiSadrzajId = table.Column<int>(type: "integer", nullable: false),
                    Ocena = table.Column<int>(type: "integer", nullable: false),
                    Opis = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Objavljena = table.Column<bool>(type: "boolean", nullable: false),
                    Stanje = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recenzija_Korisnik_UrednikId",
                        column: x => x.UrednikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recenzija_MuzickiSadrzaj_MuzickiSadrzajId",
                        column: x => x.MuzickiSadrzajId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MuzickiSadrzajPlejlista",
                columns: table => new
                {
                    MuzickiSadrzajiId = table.Column<int>(type: "integer", nullable: false),
                    PlejlistaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuzickiSadrzajPlejlista", x => new { x.MuzickiSadrzajiId, x.PlejlistaId });
                    table.ForeignKey(
                        name: "FK_MuzickiSadrzajPlejlista_MuzickiSadrzaj_MuzickiSadrzajiId",
                        column: x => x.MuzickiSadrzajiId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuzickiSadrzajPlejlista_Plejlista_PlejlistaId",
                        column: x => x.PlejlistaId,
                        principalTable: "Plejlista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IzvodjacZanr",
                columns: table => new
                {
                    IzvodjacId = table.Column<int>(type: "integer", nullable: false),
                    ZanreviId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzvodjacZanr", x => new { x.IzvodjacId, x.ZanreviId });
                    table.ForeignKey(
                        name: "FK_IzvodjacZanr_Izvodjac_IzvodjacId",
                        column: x => x.IzvodjacId,
                        principalTable: "Izvodjac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IzvodjacZanr_Zanr_ZanreviId",
                        column: x => x.ZanreviId,
                        principalTable: "Zanr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MuzickiSadrzajZanr",
                columns: table => new
                {
                    MuzickiSadrzajId = table.Column<int>(type: "integer", nullable: false),
                    ZanreviId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuzickiSadrzajZanr", x => new { x.MuzickiSadrzajId, x.ZanreviId });
                    table.ForeignKey(
                        name: "FK_MuzickiSadrzajZanr_MuzickiSadrzaj_MuzickiSadrzajId",
                        column: x => x.MuzickiSadrzajId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuzickiSadrzajZanr_Zanr_ZanreviId",
                        column: x => x.ZanreviId,
                        principalTable: "Zanr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlejlistaZanr",
                columns: table => new
                {
                    PlejlistaId = table.Column<int>(type: "integer", nullable: false),
                    ZanroviId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlejlistaZanr", x => new { x.PlejlistaId, x.ZanroviId });
                    table.ForeignKey(
                        name: "FK_PlejlistaZanr_Plejlista_PlejlistaId",
                        column: x => x.PlejlistaId,
                        principalTable: "Plejlista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlejlistaZanr_Zanr_ZanroviId",
                        column: x => x.ZanroviId,
                        principalTable: "Zanr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IzvodjacReklama",
                columns: table => new
                {
                    IzvodjaciId = table.Column<int>(type: "integer", nullable: false),
                    ReklamaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzvodjacReklama", x => new { x.IzvodjaciId, x.ReklamaId });
                    table.ForeignKey(
                        name: "FK_IzvodjacReklama_Izvodjac_IzvodjaciId",
                        column: x => x.IzvodjaciId,
                        principalTable: "Izvodjac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IzvodjacReklama_Reklama_ReklamaId",
                        column: x => x.ReklamaId,
                        principalTable: "Reklama",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Glas_GlasanjeId",
                table: "Glas",
                column: "GlasanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Glas_KorisnikId",
                table: "Glas",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Glas_MuzickiSadrzajId",
                table: "Glas",
                column: "MuzickiSadrzajId");

            migrationBuilder.CreateIndex(
                name: "IX_GlasanjeMuzickiSadrzaj_OpcijeZaGlasanjeId",
                table: "GlasanjeMuzickiSadrzaj",
                column: "OpcijeZaGlasanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Izvodi_IzvodjacId",
                table: "Izvodi",
                column: "IzvodjacId");

            migrationBuilder.CreateIndex(
                name: "IX_Izvodi_MuzickiSadrzajId",
                table: "Izvodi",
                column: "MuzickiSadrzajId");

            migrationBuilder.CreateIndex(
                name: "IX_IzvodjacMultimedijalniSadrzaj_MultimedijalniSadrzajiId",
                table: "IzvodjacMultimedijalniSadrzaj",
                column: "MultimedijalniSadrzajiId");

            migrationBuilder.CreateIndex(
                name: "IX_IzvodjacReklama_ReklamaId",
                table: "IzvodjacReklama",
                column: "ReklamaId");

            migrationBuilder.CreateIndex(
                name: "IX_IzvodjacZanr_ZanreviId",
                table: "IzvodjacZanr",
                column: "ZanreviId");

            migrationBuilder.CreateIndex(
                name: "IX_MultimedijalniSadrzajMuzickiSadrzaj_MuzickiSadrzajId",
                table: "MultimedijalniSadrzajMuzickiSadrzaj",
                column: "MuzickiSadrzajId");

            migrationBuilder.CreateIndex(
                name: "IX_MuzickiSadrzajMuzickiSadrzaj_MuzickiSadrzajiId",
                table: "MuzickiSadrzajMuzickiSadrzaj",
                column: "MuzickiSadrzajiId");

            migrationBuilder.CreateIndex(
                name: "IX_MuzickiSadrzajPlejlista_PlejlistaId",
                table: "MuzickiSadrzajPlejlista",
                column: "PlejlistaId");

            migrationBuilder.CreateIndex(
                name: "IX_MuzickiSadrzajZanr_ZanreviId",
                table: "MuzickiSadrzajZanr",
                column: "ZanreviId");

            migrationBuilder.CreateIndex(
                name: "IX_PlejlistaZanr_ZanroviId",
                table: "PlejlistaZanr",
                column: "ZanroviId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregled_KorisnikId",
                table: "Pregled",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Pregled_MuzickiSadrzajId",
                table: "Pregled",
                column: "MuzickiSadrzajId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_MuzickiSadrzajId",
                table: "Recenzija",
                column: "MuzickiSadrzajId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzija_UrednikId",
                table: "Recenzija",
                column: "UrednikId");

            migrationBuilder.CreateIndex(
                name: "IX_Reklama_MultimedijalniSadrzajId",
                table: "Reklama",
                column: "MultimedijalniSadrzajId");

            migrationBuilder.CreateIndex(
                name: "IX_Zanr_NadZanrID",
                table: "Zanr",
                column: "NadZanrID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Glas");

            migrationBuilder.DropTable(
                name: "GlasanjeMuzickiSadrzaj");

            migrationBuilder.DropTable(
                name: "Izvodi");

            migrationBuilder.DropTable(
                name: "IzvodjacMultimedijalniSadrzaj");

            migrationBuilder.DropTable(
                name: "IzvodjacReklama");

            migrationBuilder.DropTable(
                name: "IzvodjacZanr");

            migrationBuilder.DropTable(
                name: "MultimedijalniSadrzajMuzickiSadrzaj");

            migrationBuilder.DropTable(
                name: "MuzickiSadrzajMuzickiSadrzaj");

            migrationBuilder.DropTable(
                name: "MuzickiSadrzajPlejlista");

            migrationBuilder.DropTable(
                name: "MuzickiSadrzajZanr");

            migrationBuilder.DropTable(
                name: "PlejlistaZanr");

            migrationBuilder.DropTable(
                name: "Pregled");

            migrationBuilder.DropTable(
                name: "Recenzija");

            migrationBuilder.DropTable(
                name: "Glasanje");

            migrationBuilder.DropTable(
                name: "Reklama");

            migrationBuilder.DropTable(
                name: "Izvodjac");

            migrationBuilder.DropTable(
                name: "Plejlista");

            migrationBuilder.DropTable(
                name: "Zanr");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "MuzickiSadrzaj");

            migrationBuilder.DropTable(
                name: "MultimedijalniSadrzaj");
        }
    }
}
