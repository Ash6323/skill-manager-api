using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeSkillManager.Data.Migrations
{
    public partial class MigrationV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IsActive",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Admins");
        }
    }
}
