using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberMind_API.Migrations
{
    /// <inheritdoc />
    public partial class add_Score_ChallengeDone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reponses_ChallengeDones_ChallengeDoneId",
                table: "Reponses");

            migrationBuilder.DropIndex(
                name: "IX_Reponses_ChallengeDoneId",
                table: "Reponses");

            migrationBuilder.DropColumn(
                name: "ChallengeDoneId",
                table: "Reponses");

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "ChallengeDones",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSubmit",
                table: "ChallengeDones",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "ChallengeDones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "ChallengeDones");

            migrationBuilder.DropColumn(
                name: "DateSubmit",
                table: "ChallengeDones");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "ChallengeDones");

            migrationBuilder.AddColumn<uint>(
                name: "ChallengeDoneId",
                table: "Reponses",
                type: "int unsigned",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reponses_ChallengeDoneId",
                table: "Reponses",
                column: "ChallengeDoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reponses_ChallengeDones_ChallengeDoneId",
                table: "Reponses",
                column: "ChallengeDoneId",
                principalTable: "ChallengeDones",
                principalColumn: "Id");
        }
    }
}
