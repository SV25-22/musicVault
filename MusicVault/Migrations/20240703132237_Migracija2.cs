using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicVault.Migrations
{
    /// <inheritdoc />
    public partial class Migracija2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zanr_Zanr_NadZanrID",
                table: "Zanr");

            migrationBuilder.DropIndex(
                name: "IX_Zanr_NadZanrID",
                table: "Zanr");

            migrationBuilder.RenameColumn(
                name: "NadZanrID",
                table: "Zanr",
                newName: "NadZanrId");

            migrationBuilder.CreateIndex(
                name: "IX_Zanr_NadZanrId",
                table: "Zanr",
                column: "NadZanrId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Zanr_Zanr_NadZanrId",
                table: "Zanr",
                column: "NadZanrId",
                principalTable: "Zanr",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Zanr_Zanr_NadZanrId",
                table: "Zanr");

            migrationBuilder.DropIndex(
                name: "IX_Zanr_NadZanrId",
                table: "Zanr");

            migrationBuilder.RenameColumn(
                name: "NadZanrId",
                table: "Zanr",
                newName: "NadZanrID");

            migrationBuilder.CreateIndex(
                name: "IX_Zanr_NadZanrID",
                table: "Zanr",
                column: "NadZanrID");

            migrationBuilder.AddForeignKey(
                name: "FK_Zanr_Zanr_NadZanrID",
                table: "Zanr",
                column: "NadZanrID",
                principalTable: "Zanr",
                principalColumn: "Id");
        }
    }
}
