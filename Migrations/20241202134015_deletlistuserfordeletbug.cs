using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberMind_API.Migrations
{
    /// <inheritdoc />
    public partial class deletlistuserfordeletbug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Etablissements_EtablissementId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Etablissements_EtablissementId",
                table: "Users",
                column: "EtablissementId",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Etablissements_EtablissementId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Etablissements_EtablissementId",
                table: "Users",
                column: "EtablissementId",
                principalTable: "Etablissements",
                principalColumn: "Id");
        }
    }
}
