using Microsoft.EntityFrameworkCore.Migrations;

namespace EvenementsAPI.Migrations
{
    public partial class ModfifManyToManyCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Evenements_EvenementId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_EvenementId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "EvenementId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategorieEvenement",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    EvenementsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorieEvenement", x => new { x.CategoriesId, x.EvenementsId });
                    table.ForeignKey(
                        name: "FK_CategorieEvenement_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorieEvenement_Evenements_EvenementsId",
                        column: x => x.EvenementsId,
                        principalTable: "Evenements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorieEvenement_EvenementsId",
                table: "CategorieEvenement",
                column: "EvenementsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorieEvenement");

            migrationBuilder.AddColumn<int>(
                name: "EvenementId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_EvenementId",
                table: "Categories",
                column: "EvenementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Evenements_EvenementId",
                table: "Categories",
                column: "EvenementId",
                principalTable: "Evenements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
