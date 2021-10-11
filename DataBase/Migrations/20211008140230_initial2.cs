using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBase.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Parametrs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parametrs_DepartmentId",
                table: "Parametrs",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parametrs_Departaments_DepartmentId",
                table: "Parametrs",
                column: "DepartmentId",
                principalTable: "Departaments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parametrs_Departaments_DepartmentId",
                table: "Parametrs");

            migrationBuilder.DropIndex(
                name: "IX_Parametrs_DepartmentId",
                table: "Parametrs");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Parametrs");
        }
    }
}
