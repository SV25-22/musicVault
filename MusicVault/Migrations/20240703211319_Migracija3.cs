using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MusicVault.Migrations
{
    /// <inheritdoc />
    public partial class Migracija3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Izvodi");

            migrationBuilder.CreateTable(
                name: "IzvodjacMuzickiSadrzaj",
                columns: table => new
                {
                    IzvodjaciId = table.Column<int>(type: "integer", nullable: false),
                    MuzickiSadrzajId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IzvodjacMuzickiSadrzaj", x => new { x.IzvodjaciId, x.MuzickiSadrzajId });
                    table.ForeignKey(
                        name: "FK_IzvodjacMuzickiSadrzaj_Izvodjac_IzvodjaciId",
                        column: x => x.IzvodjaciId,
                        principalTable: "Izvodjac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IzvodjacMuzickiSadrzaj_MuzickiSadrzaj_MuzickiSadrzajId",
                        column: x => x.MuzickiSadrzajId,
                        principalTable: "MuzickiSadrzaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IzvodjacMuzickiSadrzaj_MuzickiSadrzajId",
                table: "IzvodjacMuzickiSadrzaj",
                column: "MuzickiSadrzajId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IzvodjacMuzickiSadrzaj");

            migrationBuilder.CreateTable(
                name: "Izvodi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IzvodjacId = table.Column<int>(type: "integer", nullable: false),
                    MuzickiSadrzajId = table.Column<int>(type: "integer", nullable: false),
                    Uloga = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Izvodi_IzvodjacId",
                table: "Izvodi",
                column: "IzvodjacId");

            migrationBuilder.CreateIndex(
                name: "IX_Izvodi_MuzickiSadrzajId",
                table: "Izvodi",
                column: "MuzickiSadrzajId");
        }
    }
}
