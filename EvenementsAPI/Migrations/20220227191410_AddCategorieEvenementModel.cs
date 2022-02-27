using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EvenementsAPI.Migrations
{
    public partial class AddCategorieEvenementModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorieEvenement_Categories_CategoriesId",
                table: "CategorieEvenement");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorieEvenement_Evenements_EvenementsId",
                table: "CategorieEvenement");

            migrationBuilder.DropForeignKey(
                name: "FK_Evenements_Villes_VilleId",
                table: "Evenements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategorieEvenement",
                table: "CategorieEvenement");

            migrationBuilder.RenameColumn(
                name: "EvenementsId",
                table: "CategorieEvenement",
                newName: "EvenementId");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "CategorieEvenement",
                newName: "CategorieId");

            migrationBuilder.RenameIndex(
                name: "IX_CategorieEvenement_EvenementsId",
                table: "CategorieEvenement",
                newName: "IX_CategorieEvenement_EvenementId");

            migrationBuilder.AlterColumn<int>(
                name: "VilleId",
                table: "Evenements",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CategorieEvenement",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategorieEvenement",
                table: "CategorieEvenement",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CategorieEvenement_CategorieId",
                table: "CategorieEvenement",
                column: "CategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategorieEvenement_Categories_CategorieId",
                table: "CategorieEvenement",
                column: "CategorieId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorieEvenement_Evenements_EvenementId",
                table: "CategorieEvenement",
                column: "EvenementId",
                principalTable: "Evenements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evenements_Villes_VilleId",
                table: "Evenements",
                column: "VilleId",
                principalTable: "Villes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategorieEvenement_Categories_CategorieId",
                table: "CategorieEvenement");

            migrationBuilder.DropForeignKey(
                name: "FK_CategorieEvenement_Evenements_EvenementId",
                table: "CategorieEvenement");

            migrationBuilder.DropForeignKey(
                name: "FK_Evenements_Villes_VilleId",
                table: "Evenements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategorieEvenement",
                table: "CategorieEvenement");

            migrationBuilder.DropIndex(
                name: "IX_CategorieEvenement_CategorieId",
                table: "CategorieEvenement");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CategorieEvenement");

            migrationBuilder.RenameColumn(
                name: "EvenementId",
                table: "CategorieEvenement",
                newName: "EvenementsId");

            migrationBuilder.RenameColumn(
                name: "CategorieId",
                table: "CategorieEvenement",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_CategorieEvenement_EvenementId",
                table: "CategorieEvenement",
                newName: "IX_CategorieEvenement_EvenementsId");

            migrationBuilder.AlterColumn<int>(
                name: "VilleId",
                table: "Evenements",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategorieEvenement",
                table: "CategorieEvenement",
                columns: new[] { "CategoriesId", "EvenementsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategorieEvenement_Categories_CategoriesId",
                table: "CategorieEvenement",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategorieEvenement_Evenements_EvenementsId",
                table: "CategorieEvenement",
                column: "EvenementsId",
                principalTable: "Evenements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evenements_Villes_VilleId",
                table: "Evenements",
                column: "VilleId",
                principalTable: "Villes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
