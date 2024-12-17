using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberMind_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Etablissements",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etablissements", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ImageEnonces",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PathImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageEnonces", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ImageReponses",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PathImage = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageReponses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Challenge",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Enonce = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EtablissementId = table.Column<uint>(type: "int unsigned", nullable: false),
                    ImageEnoncesId = table.Column<uint>(type: "int unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenge_Etablissements_EtablissementId",
                        column: x => x.EtablissementId,
                        principalTable: "Etablissements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Challenge_ImageEnonces_ImageEnoncesId",
                        column: x => x.ImageEnoncesId,
                        principalTable: "ImageEnonces",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EtablissementId = table.Column<uint>(type: "int unsigned", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mail = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    roleId = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Etablissements_EtablissementId",
                        column: x => x.EtablissementId,
                        principalTable: "Etablissements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChallengeDones",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<uint>(type: "int unsigned", nullable: false),
                    ImageReponseId = table.Column<uint>(type: "int unsigned", nullable: true),
                    ChallengeId = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeDones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeDones_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengeDones_ImageReponses_ImageReponseId",
                        column: x => x.ImageReponseId,
                        principalTable: "ImageReponses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChallengeDones_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reponses",
                columns: table => new
                {
                    Id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Score = table.Column<int>(type: "int", nullable: false),
                    reponse = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChallengeId = table.Column<uint>(type: "int unsigned", nullable: false),
                    ChallengeDoneId = table.Column<uint>(type: "int unsigned", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reponses_ChallengeDones_ChallengeDoneId",
                        column: x => x.ChallengeDoneId,
                        principalTable: "ChallengeDones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reponses_Challenge_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Challenge_EtablissementId",
                table: "Challenge",
                column: "EtablissementId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenge_ImageEnoncesId",
                table: "Challenge",
                column: "ImageEnoncesId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeDones_ChallengeId",
                table: "ChallengeDones",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeDones_ImageReponseId",
                table: "ChallengeDones",
                column: "ImageReponseId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeDones_UserId",
                table: "ChallengeDones",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reponses_ChallengeDoneId",
                table: "Reponses",
                column: "ChallengeDoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Reponses_ChallengeId",
                table: "Reponses",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EtablissementId",
                table: "Users",
                column: "EtablissementId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_roleId",
                table: "Users",
                column: "roleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reponses");

            migrationBuilder.DropTable(
                name: "ChallengeDones");

            migrationBuilder.DropTable(
                name: "Challenge");

            migrationBuilder.DropTable(
                name: "ImageReponses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ImageEnonces");

            migrationBuilder.DropTable(
                name: "Etablissements");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
