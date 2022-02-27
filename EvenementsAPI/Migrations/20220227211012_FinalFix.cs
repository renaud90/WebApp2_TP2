using Microsoft.EntityFrameworkCore.Migrations;

namespace EvenementsAPI.Migrations
{
    public partial class FinalFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Evenements_EvenementId",
                table: "Participations");

            migrationBuilder.AlterColumn<int>(
                name: "EvenementId",
                table: "Participations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Evenements_EvenementId",
                table: "Participations",
                column: "EvenementId",
                principalTable: "Evenements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Evenements_EvenementId",
                table: "Participations");

            migrationBuilder.AlterColumn<int>(
                name: "EvenementId",
                table: "Participations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Evenements_EvenementId",
                table: "Participations",
                column: "EvenementId",
                principalTable: "Evenements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
