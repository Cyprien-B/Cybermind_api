using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberMind_API.Migrations
{
    /// <inheritdoc />
    public partial class MigrationEval3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reponse",
                table: "Reponses",
                newName: "Answer");

            migrationBuilder.RenameColumn(
                name: "categories",
                table: "Challenge",
                newName: "Categories");

            migrationBuilder.AlterColumn<uint>(
                name: "EtablissementId",
                table: "Users",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(uint),
                oldType: "int unsigned",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "Reponses",
                newName: "reponse");

            migrationBuilder.RenameColumn(
                name: "Categories",
                table: "Challenge",
                newName: "categories");

            migrationBuilder.AlterColumn<uint>(
                name: "EtablissementId",
                table: "Users",
                type: "int unsigned",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "int unsigned");
        }
    }
}
