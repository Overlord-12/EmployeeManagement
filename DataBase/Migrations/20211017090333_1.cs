using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Selections_Parameters",
                table: "Selections");

            migrationBuilder.DropIndex(
                name: "IX_Selections_ParameterId",
                table: "Selections");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Selections");

            migrationBuilder.RenameColumn(
                name: "ParameterId",
                table: "Selections",
                newName: "SelectionQuery");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectionQuery",
                table: "Selections",
                newName: "ParameterId");

            migrationBuilder.AddColumn<int>(
                name: "Mark",
                table: "Selections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Selections_ParameterId",
                table: "Selections",
                column: "ParameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Selections_Parameters",
                table: "Selections",
                column: "ParameterId",
                principalTable: "Parametrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
