using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberMind_API.Migrations
{
    /// <inheritdoc />
    public partial class categorieandcodeentreprise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenge_ImageEnonces_ImageEnoncesId",
                table: "Challenge");

            migrationBuilder.DropIndex(
                name: "IX_Challenge_ImageEnoncesId",
                table: "Challenge");

            migrationBuilder.DropColumn(
                name: "ImageEnoncesId",
                table: "Challenge");

            migrationBuilder.AddColumn<uint>(
                name: "ChallengeDoneId",
                table: "Reponses",
                type: "int unsigned",
                nullable: true);

            migrationBuilder.AddColumn<uint>(
                name: "UserId",
                table: "Reponses",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "ChallengeId",
                table: "ImageEnonces",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<string>(
                name: "CodeInscription",
                table: "Etablissements",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "categories",
                table: "Challenge",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Reponses_ChallengeDoneId",
                table: "Reponses",
                column: "ChallengeDoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Reponses_UserId",
                table: "Reponses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageEnonces_ChallengeId",
                table: "ImageEnonces",
                column: "ChallengeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageEnonces_Challenge_ChallengeId",
                table: "ImageEnonces",
                column: "ChallengeId",
                principalTable: "Challenge",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reponses_ChallengeDones_ChallengeDoneId",
                table: "Reponses",
                column: "ChallengeDoneId",
                principalTable: "ChallengeDones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reponses_Users_UserId",
                table: "Reponses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageEnonces_Challenge_ChallengeId",
                table: "ImageEnonces");

            migrationBuilder.DropForeignKey(
                name: "FK_Reponses_ChallengeDones_ChallengeDoneId",
                table: "Reponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Reponses_Users_UserId",
                table: "Reponses");

            migrationBuilder.DropIndex(
                name: "IX_Reponses_ChallengeDoneId",
                table: "Reponses");

            migrationBuilder.DropIndex(
                name: "IX_Reponses_UserId",
                table: "Reponses");

            migrationBuilder.DropIndex(
                name: "IX_ImageEnonces_ChallengeId",
                table: "ImageEnonces");

            migrationBuilder.DropColumn(
                name: "ChallengeDoneId",
                table: "Reponses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reponses");

            migrationBuilder.DropColumn(
                name: "ChallengeId",
                table: "ImageEnonces");

            migrationBuilder.DropColumn(
                name: "CodeInscription",
                table: "Etablissements");

            migrationBuilder.DropColumn(
                name: "categories",
                table: "Challenge");

            migrationBuilder.AddColumn<uint>(
                name: "ImageEnoncesId",
                table: "Challenge",
                type: "int unsigned",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Challenge_ImageEnoncesId",
                table: "Challenge",
                column: "ImageEnoncesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenge_ImageEnonces_ImageEnoncesId",
                table: "Challenge",
                column: "ImageEnoncesId",
                principalTable: "ImageEnonces",
                principalColumn: "Id");
        }
    }
}
